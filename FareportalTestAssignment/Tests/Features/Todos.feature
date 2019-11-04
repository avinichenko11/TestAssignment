Feature: Todos



		In order to avoid troubles with
		The todos' management
		I want to run these tests



Scenario: Check the todo's 'compleated' status
	Given I have called Get for '/users' endpoint
		And I have received ids of 'Leanne Graham' and 'Ervin Howell'
	When I call Get for '/todos' endpoint 
	Then Response result code for Get operation is '200'
		And I can see that 'Leanne Graham' has more than '3' compleated todos and more than 'Ervin Howell'
