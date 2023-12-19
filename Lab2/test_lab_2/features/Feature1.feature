Feature: Customer Login Functionality

Scenario: Withdraw amount from balance

	Given I am on the Home page
	When I click on the Customer Login button
	Then I should see the Customer Login page
	When I log in with the name "Hermoine Granger"
	Then I should see the Customer Dashboard
	And I should see customer balance
	When I click the Withdraw button
	And I send a number that is bigger than my balance
	Then I should see the error message
	When I input a number that is suitable for my balance
	Then I should see that my balance has been reduced by the entered amount