Feature: WorkFlow

A short summary of the feature

@tag1
Scenario: PDF Workflow
	Given the following event
		| Event_Type | Process_State | TenantId | EventState |
		| HandlePdf  | Receive       | 1        | New        |

	When the event is processed

	Then the event should be in the following state
		| Event_Type | Process_State | TenantId | EventState |
		| HandlePdf  | Receive       | 1        | Completed  |
