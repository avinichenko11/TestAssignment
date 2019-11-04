Feature: Posts


		In order to avoid troubles with
		The posts' management
		I want to run these tests



Scenario: Check that new post can be added into the system
	Given I have generated object to Post
	When I call Post for '/posts' with generated data
		And I call Get for '/posts' endpoint                  
	Then Response result code for Post operation is '201'
		And I can see that generated post exists in result's list



Scenario: Check that new post can be updated into the system
	Given I have new post for 'editing'
		And I have generated object to Put
	When I call Put for '/posts' with generated data
		And I call Get for '/posts' endpoint 
	Then Response result code for Put operation is '204'
		And I can see that edited post exists in result's list



Scenario: Check that new post can be deleted from the system
	Given I have new post for 'deleting'
	When I call Delete for '/posts'
		And I call Get for '/posts' endpoint 
	Then Response result code for Delete operation is '200'
		And I can see that new post doesn`t exist in result's list


Scenario: Check that post posted by specified user
	Given I have called Get for '/users' endpoint
		And I have received id of 'Patricia Lebsack'
	When I call Get for '/posts' endpoint 
	Then Response result code for Get operation is '200'
		And I can see that post with title 'eos dolorem iste accusantium est eaque quam' belongs to 'Patricia Lebsack'