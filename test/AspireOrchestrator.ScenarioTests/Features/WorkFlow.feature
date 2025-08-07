Feature: WorkFlow

A short summary of the feature

@tag1
Scenario: Receipt Workflow by event
	Given the following event
		| Event_Type    | Process_State | TenantId | EventState | FlowId |
		| HandleReceipt | Receive       | 1        | New        | 123    |

	When the next event is processed

	Then EventEntity table contains rows
		| Event_Type    | Process_State | TenantId | EventState | FlowId |
		| HandleReceipt | Receive       | 1        | Completed  | 123    |
		| HandleReceipt | Parse         | 1        | New        | 123    |
	When the next event is processed

	Then EventEntity table contains rows
		| Event_Type    | Process_State | TenantId | EventState | FlowId |
		| HandleReceipt | Receive       | 1        | Completed  | 123    |
		| HandleReceipt | Parse         | 1        | Completed  | 123    |
		| HandleReceipt | Validate      | 1        | New        | 123    |

	When the next event is processed

	Then EventEntity table contains rows
		| Event_Type    | Process_State  | TenantId | EventState | FlowId |
		| HandleReceipt | Receive        | 1        | Completed  | 123    |
		| HandleReceipt | Parse          | 1        | Completed  | 123    |
		| HandleReceipt | Validate       | 1        | Completed  | 123    |
		| HandleReceipt | ProcessPayment | 1        | New        | 123    |

	When the next event is processed

	Then EventEntity table contains rows
		| Event_Type    | Process_State   | TenantId | EventState | FlowId |
		| HandleReceipt | Receive         | 1        | Completed  | 123    |
		| HandleReceipt | Parse           | 1        | Completed  | 123    |
		| HandleReceipt | Validate        | 1        | Completed  | 123    |
		| HandleReceipt | ProcessPayment  | 1        | Completed  | 123    |
		| HandleReceipt | TransferResult  | 1        | New        | 123    |

	When the next event is processed

	Then EventEntity table contains rows
		| Event_Type    | Process_State     | TenantId | EventState | FlowId |
		| HandleReceipt | Receive           | 1        | Completed  | 123    |
		| HandleReceipt | Parse             | 1        | Completed  | 123    |
		| HandleReceipt | Validate          | 1        | Completed  | 123    |
		| HandleReceipt | ProcessPayment    | 1        | Completed  | 123    |
		| HandleReceipt | TransferResult    | 1        | Completed  | 123    |
		| HandleReceipt | WorkFlowCompleted | 1        | Completed  | 123    |

@tag1
Scenario: Receipt Workflow by FlowId
	Given the following event
		| Event_Type    | Process_State | TenantId | EventState | FlowId |
		| HandleReceipt | Receive       | 1        | New        | 124    |

	When the flow has been processed

	Then EventEntity table contains rows
		| Event_Type    | Process_State     | TenantId | EventState | FlowId |
		| HandleReceipt | Receive           | 1        | Completed  | 124    |
		| HandleReceipt | Parse             | 1        | Completed  | 124    |
		| HandleReceipt | Validate          | 1        | Completed  | 124    |
		| HandleReceipt | ProcessPayment    | 1        | Completed  | 124    |
		| HandleReceipt | TransferResult    | 1        | Completed  | 124    |
		| HandleReceipt | WorkFlowCompleted | 1        | Completed  | 124    |

Scenario: Deposit Workflow by FlowId
	Given the following event
		| Event_Type    | Process_State | TenantId | EventState | FlowId |
		| HandleDeposit | Receive       | 1        | New        | 124    |

	When the flow has been processed

	Then EventEntity table contains rows
		| Event_Type    | Process_State     | TenantId | EventState | FlowId |
		| HandleDeposit | Receive           | 1        | Completed  | 124    |
		| HandleDeposit | Parse             | 1        | Completed  | 124    |
		| HandleDeposit | ProcessPayment    | 1        | Completed  | 124    |
		| HandleDeposit | TransferResult    | 1        | Completed  | 124    |
		| HandleDeposit | WorkFlowCompleted | 1        | Completed  | 124    |