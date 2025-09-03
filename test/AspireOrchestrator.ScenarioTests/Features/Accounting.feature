Feature: Accounting

A short summary of the feature

Scenario: Full Accounting Flow
	When IpStandard fil "IP-standard 2025.txt" er i storage
	When filen er parset
	Then ReceiptDetail tabel indeholder
	| Cvr      | Cpr         | LaborAgreementNumber | FromDate   | ToDate     | PaymentDate | PaymentReference | Amount  | ReconcileStatus | Valid | CustomerNumber | TotalContributionRate | ContributionRateFromDate | EmploymentTerminationDate | SubmissionDate | DocumentType | ReceiptType |
	| 10008328 | 130250-0009 | 10000                | 2025-07-01 | 2025-07-31 | 2025-07-25  | 10008328         | 4796.56 | Open            | False | 29247          | 10.95                 | 2007-01-01               | 2016-06-01                | 2025-07-25     | IpStandard   | Payment     |


	When Camt53 fil "Camt53 IP 2025.xml" er i storage
	When filen er parset
	Then Deposit tabel indeholder
	| ReconcileStatus | Amount  | PaymentReference | AccountNumber  | TrxDate    | ValDate    | Currency | ReconcileStatus |
	| Paid            | 4796.56 | 10008328         | 22768976202121 | 2025-07-25 | 2025-07-25 | DKK      | Paid            |

	Then PostingJournal tabel indeholder
	| PostingDate | PostingPurpose   |
	| 2025-07-25  | Deposit received |

	Then PostingEntry tabel indeholder
		| PostingAccount | AccountType   | BankTrxDate | BankValDate | CreditAmount | DebitAmount | PostingDocumentType | Currency | PostingMessage |
		| 22768976202121 | BankAccount   | 2025-07-25  | 2025-07-25  | 0            | 4796.56     | Deposit             | DKK      | 10008328       |
		| Offset         | OffsetAccount | 2025-07-25  | 2025-07-25  | 4796.56      | 0           | Deposit             | DKK      | 10008328       |

	When IpStandard dokumenter er afstemt
	Then ReceiptDetail tabel indeholder
	| Cvr      | Cpr         | LaborAgreementNumber | FromDate   | ToDate     | PaymentDate | PaymentReference | Amount  | ReconcileStatus | 
	| 10008328 | 130250-0009 | 10000                | 2025-07-01 | 2025-07-31 | 2025-07-25  | 10008328         | 4796.56 | Paid            | 

	Then Deposit tabel indeholder
	| ReconcileStatus | Amount  | PaymentReference | AccountNumber  | TrxDate    | ValDate    | Currency | ReconcileStatus |
	| Closed          | 4796.56 | 10008328         | 22768976202121 | 2025-07-25 | 2025-07-25 | DKK      | Closed          |

	Then PostingJournal tabel indeholder
	| PostingDate | PostingPurpose   |
	| 2025-07-25  | Deposit received |
	| 2025-07-25  | Deposit closed   |

	Then PostingEntry tabel indeholder
	| PostingAccount | AccountType   | BankTrxDate | BankValDate | CreditAmount | DebitAmount | PostingDocumentType | Currency | PostingMessage   |
	| 22768976202121 | BankAccount   | 2025-07-25  | 2025-07-25  | 0            | 4796.56     | Deposit             | DKK      | 10008328         |
	| Offset         | OffsetAccount | 2025-07-25  | 2025-07-25  | 4796.56      | 0           | Deposit             | DKK      | 10008328         |
	| 130250-0009    | Person        | 2025-07-25  | 2025-07-25  | 0            | 4796.56     | ReceiptDetail       | DKK      | 10008328         |
	| 22768976202121 | BankAccount   | 2025-07-25  | 2025-07-25  | 4796.56      | 0           | Deposit             | DKK      | Closing 10008328 |

