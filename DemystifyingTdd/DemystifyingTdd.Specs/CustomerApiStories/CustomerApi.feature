Feature: CustomerApi
	As an api user
	In order to interact with customer data
	I want an api to expose the customer data

Scenario: Get Customer
	Given I have the following customer data
	| FirstName | LastName  | Address         | City   | State | ZipCode | FavoriteColor |
	| John      | Doe       | 513 4th S Dr.   | Omaha  | NE    | 55555   | Pink          |
	| Jane      | Doe       | 513 4th S Dr.   | Omaha  | NE    | 55555   | Green         |
	| Felicia   | Esmeralda | 123 Unicorn Dr. | Wayne  | KS    | 78945   | Black         |
	| Oberon    | Dane      | 445 West Park   | Moon   | WA    | 13268   | Brown         |
	| Hermia    | Amarillo  | 9812 Reinke     | Neward | PA    | 64512   | Blue          |
	And the following subscriptions
	| Customer        | Title               |
	| John Doe        | Wall Street Journal |
	| John Doe        | New York Times      |
	| Oberon Dane     | Home and Garden     |
	| Hermia Amarillo | Unicorns Anonymous  |
	| Hermia Amarillo | MSDN                |
	| Hermia Amarillo | Washington Journal  |
	And a customers url of "/api/customers"
	When I request the customer
	Then Ok status code is returned
	And the customer data is returned
