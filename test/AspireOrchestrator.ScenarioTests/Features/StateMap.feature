Feature: StateMachine

Testing the State Machine

@EventStates
Scenario Outline: GetNextState
	When the event with EventType <Event_Type> and ProcessState <Process_State>
	Then the ProcessState of the next event is <Next_Process_State>

	Examples: 
	| Event_Type      | Process_State     | Next_Process_State |
	| HandleReceipt   | Receive           | Parse              |
	| HandleReceipt   | Parse             | Validate           |
	| HandleReceipt   | Validate          | ProcessPayment     |
	| HandleReceipt   | ProcessPayment    | TransferResult     |
	| HandleReceipt   | TransferResult    | WorkFlowCompleted  |
	| HandleReceipt   | WorkFlowCompleted | WorkFlowCompleted  |
	| HandleDeposit   | Receive           | Parse              |
	| HandleDeposit   | Parse             | ProcessPayment     |
	| HandleDeposit   | ProcessPayment    | TransferResult     |
	| HandleDeposit   | TransferResult    | WorkFlowCompleted  |
	| HandleDeposit   | WorkFlowCompleted | WorkFlowCompleted  |
	| ValidateReceipt | Validate          | ProcessPayment     |
	| ValidateReceipt | ProcessPayment    | TransferResult     |
	| ValidateReceipt | TransferResult    | WorkFlowCompleted  |
	| ValidateReceipt | WorkFlowCompleted | WorkFlowCompleted  |
