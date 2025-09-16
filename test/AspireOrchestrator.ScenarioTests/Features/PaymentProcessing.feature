Feature: PaymentProcessing

A short summary of the feature

Scenario: Match IpStandard
Given ReceiptDetail tabel indeholder
| Cvr      | Cpr         | LaborAgreementNumber | FromDate   | ToDate     | PaymentDate | PaymentReference | Amount  | ReconcileStatus | Valid | CustomerNumber | TotalContributionRate | ContributionRateFromDate | EmploymentTerminationDate | SubmissionDate | DocumentType | ReceiptType |
| 10008328 | 130250-0009 | 10000                | 2025-07-01 | 2025-07-31 | 2025-07-25  | 10008328         | 4796.56 | Open            | False | 29247          | 10.95                 | 2007-01-01               | 2016-06-01                | 2025-07-25     | IpStandard   | Payment     |

Given Deposit tabel indeholder
| ReconcileStatus | Amount  | PaymentReference | AccountNumber  | TrxDate    | ValDate    | Currency |
| Paid            | 4796.56 | 10008328         | 22768976202121 | 2025-07-25 | 2025-07-25 | DKK      |

When IpStandard dokumenter er afstemt
Then ReceiptDetail tabel indeholder
| Cvr      | Cpr         | LaborAgreementNumber | FromDate   | ToDate     | PaymentDate | PaymentReference | Amount  | ReconcileStatus | Valid | CustomerNumber | TotalContributionRate | ContributionRateFromDate | EmploymentTerminationDate | SubmissionDate | DocumentType | ReceiptType |
| 10008328 | 130250-0009 | 10000                | 2025-07-01 | 2025-07-31 | 2025-07-25  | 10008328         | 4796.56 | Paid            | False | 29247          | 10.95                 | 2007-01-01               | 2016-06-01                | 2025-07-25     | IpStandard   | Payment     |

Then Deposit tabel indeholder
| ReconcileStatus | Amount  | PaymentReference | AccountNumber  | TrxDate    | ValDate    | Currency |
| Closed          | 4796.56 | 10008328         | 22768976202121 | 2025-07-25 | 2025-07-25 | DKK      |
