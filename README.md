
# The Movie Gallery

## Design Document - The Movie Gallery      
https://moviegallery20210416202022.azurewebsites.net/
 --
 ## Team Composition 
 
 -Rutuja Sawant 
 -Mohini Chakravarty    
 -Gaytri Khatwani
 
 --
 
 ## Introduction  

 Do you want to find out which movies and shows have the top ratings? Let us do it.

-	Find out the movie and shows launched in a particular year
-	Find out the movies and shows having a certain rating

---  

## Data Feeds  

- IMDb top 250 movies Data: https://imdb-api.com/en/API/Top250Movies/k_81ggrpaf   
- IMDb top 250 shows Data: https://imdb-api.com/en/API/Top250TVs/k_81ggrpaf

### External Group Api
- Nobel Laureates: https://themodernilluminati.azurewebsites.net/ExposeApi?country=IN
---  

## Functional Requirements  

#### Requirement 1.1  

**Scenario:**  As a user I want to find top 250 movies and shows sorted by rank
**Dependencies:** Movies and Shows datasets should be available

**Given** the datasets are available  
**When** i land on search pages  
**Then** Top 250 movies and shows data be available in tabular format  

#### Requirement 1.2

Scenario: As a user I want to find the list of all movies and shows relaeased in a particular year
**Dependencies:** Movies and Shows datasets should be available, user should enter data in valid year format

**Given** the datasets are available  
**When** when i enter data in correct year format  
**Then** I should see movies and shows from that year if year is valid, and appropriate error message for invalid year  

#### Requirement 1.3 

**Scenario:**  As a user I want to find all movies and shows having a particular rating
**Dependencies:** Movies and Shows datasets should be available, user should select rating from dropdown

**Given** the datasets are available  
**When** when i select rating from dropdown  
**Then** I should see movies and shows from that rating, appropriate error message for invalid rating and message for missing data  

#### Requirement 1.4

**Scenario:**  As an external app i want to receive a JSON object of top rated movies by year
**Dependencies:** App is up and running on azure url https://moviegallery20210416202022.azurewebsites.net/

**Given** the app is hosted and available    
**When** when i call the url     
**Then** I should receive JSON output for top rated movies from the year i enter https://moviegallery20210416202022.azurewebsites.net/SearchByYear?year=2021       
  
---
## Team Roles 

 -Rutuja Sawant: *Frontend developer*   
 -Mohini Chakravarty: *Scrum Master*    
 -Gaytri Khatwani: *Backend Developer* 
 
## Meeting Time
 10 AM to 1 PM on Mondays and Fridays (Hybrid)  
