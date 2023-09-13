# SteamLookupAPI

## Requirements
- Micro-service architecture
- Use a 3rd party location-from-IP or other lookup service eg get the weather from a location
- REST interface
- Service should maintain a cache
- Service should maintain a persistent store of the looked-up values
- Include unit tests
- Include Integration tests
- C# .NET 7


## 3rd Party Service
When looking at which 3rd party to use I wanted to use something a little more interesting that an IP or the weather and picked something a little more personal to me, which is the Steam API.   
There are different APIs we can use (with a token) depending on what information you want to find, and there is a .NET wrapped called [SteamWebAPI2](https://github.com/babelshift/SteamWebAPI2) which makes it much easier to use the Steam Web API (which isn't well documented). 

## Persistant Store
Thinking about the data I need to store and given that realisitcally it'll be a simple list of query parameters with a timestamp (and maybe in the future user details when access control is added) I'd like to use ElasticSearch. It's non-relational because I don't think we'll need any relationships, and it's pretty good at fast processing data. 

### Update
Given I have a time limit on this project, I've chosen to remove ES and add SQL instead. This is only due to easier set up and needing to get the project finished - given unlimited time I'd definitely implement ES for this because having a SQL DB here is overkill. I've added in a `Query` object to store the requests coming in with a json serialised list of query parameters. I've also added a `User` object to track their createdDate and latest status, though this is unnecessary for this small project.