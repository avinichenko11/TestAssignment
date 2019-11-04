Feature: Photos



		In order to avoid troubles with
		The photos' management
		I want to run these tests



Scenario: Check that photo belongs to specified user
	Given I have called Get for '/users' endpoint
		And I have received id by 'Sincere@april.biz' email
	When I call Get for '/photos' endpoint 
	Then Response result code for Get operation is '200'
		And I can see that photo with title 'ad et natus qui' text belongs to 'Sincere@april.biz'


Scenario: Check that image from Photo is not corrupted
	Given I have called Get for '/photos' endpoint
		And I have received photo with id '4'
	When I get the image of the received Photo
	Then I see that received image matches to expected one