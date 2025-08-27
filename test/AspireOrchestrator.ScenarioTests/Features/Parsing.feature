Feature: Parsing

Parse different information and payment files

Scenario: Parse IpStandard
	When IpStandard fil "IP-standard 2025.txt" er i storage
	When filen er parset
	Then ReceiptDetail tabel indeholder
| Cvr      | Cpr         | LaborAgreementNumber | FromDate   | ToDate     | PaymentDate | PaymentReference | Amount  | ReconcileStatus | Valid | CustomerNumber | TotalContributionRate | ContributionRateFromDate | EmploymentTerminationDate | SubmissionDate | DocumentType | ReceiptType |
| 10008328 | 130250-0009 | 10000                | 2025-07-01 | 2025-07-31 | 2025-07-25  | 10008328         | 4796.56 | Open            | False | 29247          | 10.95                 | 2007-01-01               | 2016-06-01                | 2025-07-25     | IpStandard   | Payment     |


Scenario: Parse Camt53
	When Camt53 fil "Camt53 IP 2025.xml" er i storage
	When filen er parset
	Then Deposit tabel indeholder
	| ReconcileStatus | Amount  | PaymentReference | AccountNumber  | TrxDate    | ValDate    | Currency |
	| Paid            | 4796.56 | 000000100083286  | 22768976202121 | 2025-07-25 | 2025-07-25 | DKK      |