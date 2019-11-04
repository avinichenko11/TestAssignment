Feature: Comments


		In order to avoid troubles with
		The comments' management
		I want to run these tests




Scenario: Check that email address belongs to user who left comment
	When I call Get for '/comments' endpoint 
	Then Response result code for Get operation is '200'
		And I can see that user who left a comment with 'ipsum dolorem' text is 'Marcia@name.biz'