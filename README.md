# MyLedgerApp

This is a small REST API project of a Ledger Application structured in a Service Oriented Arquitecture. 

### Data Structure
- Client (contains Ledgers)
- Employee (is necessary to open a Ledger)
- Ledger (contains Transactions)
- Transactions (can be a type Deposit or Withdrawal)

### Usage
1) Start creating in this order: Users -> Ledgers -> Transactions
2) Any Domain Data can be fetch, created and deleted. Only Users can be Updated.
