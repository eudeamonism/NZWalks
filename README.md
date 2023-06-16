# NZWalks

## .NET Web APi 

This API houses 15 walks, trails, that show the destination's name, length in kilometers, description, image, region code and image all with supporting ids.



## Features

Utilizes DTO structure so a user doesn't have access to the domain model at any time. Filtering, sorting, and pagination is included in the **GET** all walks.
Incorporates Repositories structure to keep Controllers thin and limit access to the database. 

## APIs

**Regions:** Get(All), Get(one), Post(Create), Put(update), and Delete

**Walks:** Get(All), Get(one), Post(Create), Put(update), and Delete

## Folder Structure
<pre>
NZWALKS.API
|__ Properties
|__ Controllers
|__ CustomActionFilters
|__ Data
|__ Mappings
|__ Migrations
|__ Models
    |__ Domain
    |__ DTO
|__ Repositories
|__ Services
|__ Startup.cs
|__ Web.config
</pre>



