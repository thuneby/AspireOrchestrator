using AspireOrchestrator.Core.OrchestratorModels;
using AspireOrchestrator.Domain.Models;
using AspireOrchestrator.Parsing.Interfaces;
using AspireOrchestrator.Parsing.Models.Nordea;
using AspireOrchestrator.Parsing.Models.NordeaRW;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Xml;

namespace AspireOrchestrator.Parsing.Business
{
    public class Camt53Parser(ILoggerFactory loggerFactory): IDepositParser
    {
        public async Task<IEnumerable<Deposit>> ParseAsync(Stream payload, DocumentType documentType)
        {
            var document = GetCamt53DocumentFromStream(payload);
            var statement = ParseStatement(document.BkToCstmrStmt.Stmt);
            statement.Entries = ParseTransactionEntries(document.BkToCstmrStmt.Stmt);
            statement.MsgId = document.BkToCstmrStmt.GrpHdr.MsgId;
            var deposits = new List<Deposit>();
            foreach (var entry in statement.Entries)
            {
                var deposit = new Deposit
                {
                    ReconcileStatus = ReconcileStatus.Paid,
                    Amount = entry.AmtValue,
                    Currency = entry.AmtCcy,
                    AccountNumber = statement.AcctId,
                    AccountReference = entry.AcctSvcrRef,
                    Message = entry.RmtInf,
                    TrxDate = entry.BookgDtDt,
                    ValDate = entry.ValDtDt,
                    PaymentReference = entry.AcctSvcrRef == "INFO-OVF" ? entry.AcctSvcrRef : entry.PmtInfId
                };
                if (entry.CdtDbtInd == "DBIT")
                    deposit.Amount = -deposit.Amount;
                deposits.Add(deposit);
            }
            return deposits;
        }


        private static Document GetCamt53DocumentFromStream(Stream payload)
        {
            using var streamReader = new StreamReader(payload);
            var xmlTextReader = new XmlTextReader(streamReader);
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Document));
            var document = (Document)xmlSerializer.Deserialize(xmlTextReader)!;
            return document;
        }

        private Camt53Statement ParseStatement(AccountStatement2[] camtStatement)
        {
            var statement = new Camt53Statement();

            if (camtStatement.Length == 0) return statement;
            var config = new MapperConfiguration(cfg => 
                cfg.CreateMap<AccountStatement2, Camt53Statement>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                , loggerFactory);
            var mapper = config.CreateMapper();
            var statementList = camtStatement.Select(x => mapper.Map<AccountStatement2, Camt53Statement>(x)).ToList();
            statement = statementList.FirstOrDefault();
            if (statement == null) return new Camt53Statement();
            var stmt = camtStatement.FirstOrDefault();
            if (stmt == null) return statement;
            statement.OpeningBalanceAmount = stmt.Bal.First().Amt.Value;
            statement.OpeningBalanceDate = stmt.Bal.First().Dt.Item;
            statement.OpeningBalanceCdtDbtInd = stmt.Bal.First().CdtDbtInd.ToString();
            statement.ClosingBalanceAmount = stmt.Bal.Last().Amt.Value;
            statement.ClosingBalanceDate = stmt.Bal.Last().Dt.Item;
            statement.ClosingBalanceCdtDbtInd = stmt.Bal.Last().CdtDbtInd.ToString();
            statement.AcctId = camtStatement.First().Acct.Id.Othr.Id;
            statement.AccCcy = stmt.Acct.Ccy;
            statement.FinInstnId = stmt.Acct.Svcr.FinInstnId.BIC;
            statement.StmtId = stmt.Id;
            return statement;
        }

        private List<Camt53TransactionEntry> ParseTransactionEntries(AccountStatement2[] camtStatement)
        {
            var entries = new List<Camt53TransactionEntry>();
            if (camtStatement.Length == 0) return entries;
            var stmt = camtStatement.FirstOrDefault();
            var config = new MapperConfiguration(cfg 
                => cfg.CreateMap<ReportEntry2, Camt53TransactionEntry>(), loggerFactory);
            var mapper = config.CreateMapper();
            if (stmt == null) return entries;
            entries = stmt.Ntry.Select(x => mapper.Map<ReportEntry2, Camt53TransactionEntry>(x)).ToList();
            var i = 0;
            foreach (var entry in entries)
            {
                entry.BookgDtDt = stmt.Ntry[i].BookgDt.Item;
                entry.ValDtDt = stmt.Ntry[i].ValDt.Item;
                var ntryDtls = stmt.Ntry[i].NtryDtls.FirstOrDefault();
                var txDtls = ntryDtls?.TxDtls.FirstOrDefault();
                if (txDtls == null) continue;
                entry.PmtInfId = txDtls.Refs.PmtInfId;
                entry.RmtInf = txDtls.RmtInf.Ustrd != null ? txDtls.RmtInf.Ustrd.Aggregate("", (current, str) => current + " " + str) : String.Empty;
                i++;
            }
            return entries;
        }
    }
}
