Feature: StateMachine

Testing the State Machine

@EventStates
Scenario Outline: GetNextState
	When the event with EventType <Event_Type> and ProcessState <Process_State>
	Then the ProcessState of the next event is <Next_Process_State>

	Examples: 
	| Event_Type | Process_State     | Next_Process_State |
	| HandlePdf  | Receive           | Parse              |
	| HandlePdf  | Parse             | Convert            |
	| HandlePdf  | Convert           | Validate           |
	| HandlePdf  | Validate          | Process            |
	| HandlePdf  | Process           | GenerateReceipt    |
	| HandlePdf  | GenerateReceipt   | TransferResult     |
	| HandlePdf  | TransferResult    | WorkFlowCompleted  |
	| HandlePdf  | WorkFlowCompleted | WorkFlowCompleted  |

