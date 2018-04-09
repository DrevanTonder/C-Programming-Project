# Testing

The unit tests can be found in the BL.Test Project.

| Method Name                    | Description                              |
| ------------------------------ | ---------------------------------------- |
| RetrieveTest                   | Normal Usage                             |
| RetrieveTestStreamNull         | Throw `ArgumentNullException` when `stream` is null |
| RetrieveTestMissingItems       | Make sure that the Retrieve method returns a empty `IEnumerable` when `stream` has no items. |
| RetrieveTestIncompleteItems    | Throw `ArgumentException` when the `stream` has incomplete items. |
| SaveTest                       | Normal Usage                             |
| SaveTestStreamNull             | Throw `ArgumentNullException` when `stream` is null |
| UpdateTest                     | Normal Usage                             |
| UpdateTestItemCodeNull         | Throw `ArgumentNullException` when `itemCode` is null |
| UpdateTestItemCodeDoesNotExist | Throw `ArgumentException` when `itemCode` is not in the `ItemRepository` items dictionary |