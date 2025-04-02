# Ledger

.Net 9 application

To run this project, from directory ledger/ simply:
    <code>dotnet run --project Teya.Ledger.API/Ledger.API</code>


## Examples 

test by running:
- <code>Invoke-WebRequest -Uri 'https://localhost:5001/api/ledger/balance' | Select-Object -ExpandProperty Content</code>
- <code>Invoke-WebRequest -Uri "https://localhost:5001/api/ledger/deposit" -Method POST -Headers @{"Content-Type"="application/json"} -Body '{"amount": 100.0}' | Select-Object -ExpandProperty Content</code>

- Alternatively you can connect https://localhost:5001/swagger/index.html after spinning up the project and test the endpoints through swagger

Some assumptions:
- In memory domain objects have been used so no database connections, this means when the app is killed all data is lost 
- For the transactions a dictionary would be more efficient but Lists are a bit more flexible
- Some similar logic was skipped for example chekcing the amount on a deposit isn't over double.MaxValue is done only there and not on the withdraw, for the sake of time
- User can only deposit up to 1 trillion per transaction
- Assume transaction is always successful, if it conforms to api validation and business rules validation
