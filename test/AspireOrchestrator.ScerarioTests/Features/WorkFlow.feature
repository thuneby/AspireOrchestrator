Feature: WorkFlow

A short summary of the feature

@tag1
Scenario: PDF Workflow by event
	Given the following event
		| Event_Type | Process_State | TenantId | EventState | FlowId |
		| HandlePdf  | Receive       | 1        | New        | 123    |

	When the next event is processed

	Then EventEntity table contains rows
		| Event_Type | Process_State | TenantId | EventState | FlowId |
		| HandlePdf  | Receive       | 1        | Completed  | 123    |
		| HandlePdf  | Parse         | 1        | New        | 123    |
	When the next event is processed

	Then EventEntity table contains rows
		| Event_Type | Process_State | TenantId | EventState | FlowId |
		| HandlePdf  | Receive       | 1        | Completed  | 123    |
		| HandlePdf  | Parse         | 1        | Completed  | 123    |
		| HandlePdf  | Convert       | 1        | New        | 123    |

	When the next event is processed

	Then EventEntity table contains rows
		| Event_Type | Process_State | TenantId | EventState | FlowId |
		| HandlePdf  | Receive       | 1        | Completed  | 123    |
		| HandlePdf  | Parse         | 1        | Completed  | 123    |
		| HandlePdf  | Convert       | 1        | Completed  | 123    |
		| HandlePdf  | Validate      | 1        | New        | 123    |

	When the next event is processed

	Then EventEntity table contains rows
		| Event_Type | Process_State | TenantId | EventState | FlowId |
		| HandlePdf  | Receive       | 1        | Completed  | 123    |
		| HandlePdf  | Parse         | 1        | Completed  | 123    |
		| HandlePdf  | Convert       | 1        | Completed  | 123    |
		| HandlePdf  | Validate      | 1        | Completed  | 123    |
		| HandlePdf  | Process       | 1        | New        | 123    |

	When the next event is processed

	Then EventEntity table contains rows
		| Event_Type | Process_State   | TenantId | EventState | FlowId |
		| HandlePdf  | Receive         | 1        | Completed  | 123    |
		| HandlePdf  | Parse           | 1        | Completed  | 123    |
		| HandlePdf  | Convert         | 1        | Completed  | 123    |
		| HandlePdf  | Validate        | 1        | Completed  | 123    |
		| HandlePdf  | Process         | 1        | Completed  | 123    |
		| HandlePdf  | GenerateReceipt | 1        | New        | 123    |

	When the next event is processed

	Then EventEntity table contains rows
		| Event_Type | Process_State   | TenantId | EventState | FlowId |
		| HandlePdf  | Receive         | 1        | Completed  | 123    |
		| HandlePdf  | Parse           | 1        | Completed  | 123    |
		| HandlePdf  | Convert         | 1        | Completed  | 123    |
		| HandlePdf  | Validate        | 1        | Completed  | 123    |
		| HandlePdf  | Process         | 1        | Completed  | 123    |
		| HandlePdf  | GenerateReceipt | 1        | Completed  | 123    |
		| HandlePdf  | TransferResult  | 1        | New        | 123    |

	When the next event is processed

	Then EventEntity table contains rows
		| Event_Type | Process_State     | TenantId | EventState | FlowId |
		| HandlePdf  | Receive           | 1        | Completed  | 123    |
		| HandlePdf  | Parse             | 1        | Completed  | 123    |
		| HandlePdf  | Convert           | 1        | Completed  | 123    |
		| HandlePdf  | Validate          | 1        | Completed  | 123    |
		| HandlePdf  | Process           | 1        | Completed  | 123    |
		| HandlePdf  | GenerateReceipt   | 1        | Completed  | 123    |
		| HandlePdf  | TransferResult    | 1        | Completed  | 123    |
		| HandlePdf  | WorkFlowCompleted | 1        | Completed  | 123    |

@tag1
Scenario: PDF Workflow by FlowId
	Given the following event
		| Event_Type | Process_State | TenantId | EventState | FlowId |
		| HandlePdf  | Receive       | 1        | New        | 124    |

	When the flow has been processed

	Then EventEntity table contains rows
		| Event_Type | Process_State     | TenantId | EventState | FlowId |
		| HandlePdf  | Receive           | 1        | Completed  | 124    |
		| HandlePdf  | Parse             | 1        | Completed  | 124    |
		| HandlePdf  | Convert           | 1        | Completed  | 124    |
		| HandlePdf  | Validate          | 1        | Completed  | 124    |
		| HandlePdf  | Process           | 1        | Completed  | 124    |
		| HandlePdf  | GenerateReceipt   | 1        | Completed  | 124    |
		| HandlePdf  | TransferResult    | 1        | Completed  | 124    |
		| HandlePdf  | WorkFlowCompleted | 1        | Completed  | 124    |