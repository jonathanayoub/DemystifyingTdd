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
