Feature: SearchForDomain
	In order to test search and bying functionality 

Scenario: Search for Domain, add it to cart and proceed to Payment page
	Given I have navigated to Domain-checker page
	And I have entered karolinaHostingerTest in Domain Checker
	When I try to select domain suffix .online
	Then I check if domain is available
	When I try to add domain to cart
	Then I check if domain karolinaHostingerTest with suffix .online is added to cart
	When I try to checkout
	And fill in test users credentials
	Then I should be at Payment Method page 
