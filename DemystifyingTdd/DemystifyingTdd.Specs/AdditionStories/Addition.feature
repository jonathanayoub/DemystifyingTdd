Feature: Addition
	As an api user
	In order to find the sum of numbers
	I want the the sum to be calculated
	
Scenario: Add two numbers
	Given I want to add 50 and 70
	And A web api client
	And an additions url of "/api/additions"
	When I calculate the addition result
	Then the result should be 120

Scenario: Add a list of numbers
	Given I have the following numbers
	| number1 | number2 | number3 | number4 | number5 | number6 |
	| 12.3    | 2.2     | 0.58    | -0.72   | 1.1     | 0       | 
	And A web api client
	And an additions url of "/api/additions"
	When I calculate the addition result
	Then the result should be 15.46