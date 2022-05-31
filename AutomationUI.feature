Feature: AutomationUI

This is a set of AutomationUI Tests to test Galactico Eleven

@UI_Test
Scenario: GalacticoElevenCreateLeague
	Given The user navigates to Galactico eleven
	When The user Clicks on the create leagues tabs
	Then The users creates a league

@API_Test
Scenario: GalacticoElevenAPICreateLeague
    Given the user POSTS usernmae and password to GalacticoEleven login
	When the user POSTS a new league
	Then the user GETS the leagues to validate the new league has been created

#	Issue a GET request to https://www.galacticoeleven.com/api/league/ with the following:
#	Header – “authorization”: “Bearer [TOKEN RETURNED FROM /LOGIN REQUEST]”
#	Assert the newly created league is present in the user’s leagues