# Medialink Technical Test

## Part 1

## Medialink.Lib
Configure a dependency injection container and modify the existing classes accordingly in order to demonstrate inversion of control.
Use dapper to implement the log method as required by the MathWebClient methods so that you can write each operation to a DB.

## Part 2 - Unit testing

Use NUnit and a mocking framework such as NSubstitute/Moq so your unit tests run without depending on actual connections to DB/Service.
if your test fails you should modify the code to fix it.

## Medialink.Api.Tests
Write tests for the controller in the Medialink.Api project.

1. Add a test to check that the add method correctly sums two integers;
2. Add a test to check that the multiply method correctly multiplies to integers;
3. Add a test to check that the divide method correctly divides two integers;
4. Add a test to check that the divide method returns a 500 error when dividing by zero.

## Medialink.Lib.Tests
Write tests for the MathWebClient class in the Medialink.Lib project.

1. Add a test to check that the Add method returns an appropriate value for a range of inputs;
2. Add a test to check that the Multiply method returns an appropriate value for a range of inputs;
3. Add a test to check that the Divide method returns an appropriate value for a range of inputs;
4. Add a test to check that the Add method logs some value.