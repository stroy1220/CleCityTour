-- *************************************************************************************************
-- This script creates all of the database objects (tables, constraints, etc) for the database
-- *************************************************************************************************
--Drop table itinerary
--Drop table itineraryPlaces
--Drop table places
--Drop table userInfo






-- CREATE statements go here
create table userInfo
(
id			 int		  primary key			identity(1,1),
userName	 varchar(max) not null,
firstName    varchar(max) not null,
lastName     varchar(max) not null,
password	 varchar(max) not null,
passwordSalt varchar(max) null,
admin		 bit	      not null,

);

create table places
(
id			  int		  primary key			identity(1,1),
streetAddress varchar(max) not null,
city          varchar(max) not null,
state		  varchar(max) not null,
latitude	  varchar(max)          not null,
longitude     varchar(max)		   not null,
googleID      varchar(max) null,
detail        varchar(max) null,
placeName     varchar(max) not null,
Category      varchar(max) not null,
);    


create table itinerary
(
id			  int		    primary key			identity(1,1),
userID		  int		    not null,
name		  varchar(max)  not null,
startLocation varchar(max)  not null,
date		  datetime		null,

constraint fk_itinerary_userInfo foreign key (userID) references userInfo(id),
)

create table itineraryPlaces
(
id				  int		  primary key			identity(1,1),
itineraryID       int         not null,		
placeID			  int		  not null,

constraint fk_itineraryPlaces_itinerary foreign key (itineraryID) references itinerary(id),
constraint fk_itineraryPlaces_places foreign key (placeID) references places(id)
);
