Feature: WorkFlow

A short summary of the feature

@tag1
Scenario: Receipt Workflow by event
	Given the following event
		| Event_Type    | Process_State | TenantId | EventState | DocumentType | Parameters                                    |
		| HandleReceipt | Receive       | 1        | New        | IpStandard   | {"id":"71f1d54b-09be-463c-8676-88c5097ce4dd"} |

	When the next event is processed

	Then EventEntity table contains rows
		| Event_Type    | Process_State | TenantId | EventState | DocumentType | Parameters                                    |
		| HandleReceipt | Receive       | 1        | Completed  | IpStandard   | {"id":"71f1d54b-09be-463c-8676-88c5097ce4dd"} |
		| HandleReceipt | Parse         | 1        | New        | IpStandard   | {"id":"71f1d54b-09be-463c-8676-88c5097ce4dd"} |
	When the next event is processed

	Then EventEntity table contains rows
		| Event_Type    | Process_State | TenantId | EventState | 
		| HandleReceipt | Receive       | 1        | Completed  | 
		| HandleReceipt | Parse         | 1        | Completed  | 
		| HandleReceipt | Validate      | 1        | New        | 

	When the next event is processed

	Then EventEntity table contains rows
		| Event_Type    | Process_State  | TenantId | EventState | 
		| HandleReceipt | Receive        | 1        | Completed  | 
		| HandleReceipt | Parse          | 1        | Completed  | 
		| HandleReceipt | Validate       | 1        | Completed  | 
		| HandleReceipt | ProcessPayment | 1        | New        | 

	When the next event is processed

	Then EventEntity table contains rows
		| Event_Type    | Process_State   | TenantId | EventState | 
		| HandleReceipt | Receive         | 1        | Completed  | 
		| HandleReceipt | Parse           | 1        | Completed  | 
		| HandleReceipt | Validate        | 1        | Completed  | 
		| HandleReceipt | ProcessPayment  | 1        | Completed  | 
		| HandleReceipt | TransferResult  | 1        | New        | 

	When the next event is processed

	Then EventEntity table contains rows
		| Event_Type    | Process_State     | TenantId | EventState | 
		| HandleReceipt | Receive           | 1        | Completed  | 
		| HandleReceipt | Parse             | 1        | Completed  | 
		| HandleReceipt | Validate          | 1        | Completed  | 
		| HandleReceipt | ProcessPayment    | 1        | Completed  | 
		| HandleReceipt | TransferResult    | 1        | Completed  | 
		| HandleReceipt | WorkFlowCompleted | 1        | Completed  | 

@tag1
@ignore
Scenario: Receipt Workflow by FlowId
	Given the following event
		| Event_Type    | Process_State | TenantId | EventState | FlowId                               |
		| HandleReceipt | Receive       | 1        | New        | f80664bb-fe2c-422e-afc5-17a4eb6529cb |

	When the flow has been processed

	Then EventEntity table contains rows
		| Event_Type    | Process_State     | TenantId | EventState | FlowId                               |
		| HandleReceipt | Receive           | 1        | Completed  | f80664bb-fe2c-422e-afc5-17a4eb6529cb |
		| HandleReceipt | Parse             | 1        | Completed  | f80664bb-fe2c-422e-afc5-17a4eb6529cb |
		| HandleReceipt | Validate          | 1        | Completed  | f80664bb-fe2c-422e-afc5-17a4eb6529cb |
		| HandleReceipt | ProcessPayment    | 1        | Completed  | f80664bb-fe2c-422e-afc5-17a4eb6529cb |
		| HandleReceipt | TransferResult    | 1        | Completed  | f80664bb-fe2c-422e-afc5-17a4eb6529cb |
		| HandleReceipt | WorkFlowCompleted | 1        | Completed  | f80664bb-fe2c-422e-afc5-17a4eb6529cb |


@ignore
Scenario: Deposit Workflow by FlowId
	Given the following event
		| Event_Type    | Process_State | TenantId | EventState | FlowId                               |
		| HandleDeposit | Receive       | 1        | New        | eb91261a-f9c0-4722-aa33-2e33d76186d6 |

	When the flow has been processed

	Then EventEntity table contains rows
		| Event_Type    | Process_State     | TenantId | EventState | FlowId                               |
		| HandleDeposit | Receive           | 1        | Completed  | eb91261a-f9c0-4722-aa33-2e33d76186d6 |
		| HandleDeposit | Parse             | 1        | Completed  | eb91261a-f9c0-4722-aa33-2e33d76186d6 |
		| HandleDeposit | ProcessPayment    | 1        | Completed  | eb91261a-f9c0-4722-aa33-2e33d76186d6 |
		| HandleDeposit | TransferResult    | 1        | Completed  | eb91261a-f9c0-4722-aa33-2e33d76186d6 |
		| HandleDeposit | WorkFlowCompleted | 1        | Completed  | eb91261a-f9c0-4722-aa33-2e33d76186d6 |