Feature: Google Search
	In order to search for website results
	As a Google search user
	I want to be able to type in a search word and receive relevant Website results

Background: Go to Google Home Page
	Given I go to the Google Home Page

@SMOKE
Scenario Outline: Search for Websites on Google Search Page
	Given I enter a search word <Word> into the Google search field
	When I press the Search button
	Then results related to search word should display

Examples:

| Word       |
| Cheese     | 
| Hamburgers |



@SMOKE
Scenario: Search for Websites using one word on Google Search Page
	Given I enter a word "Blah" in the search field
	When I press the Search button
	Then results related to search word should display
