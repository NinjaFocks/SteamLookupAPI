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