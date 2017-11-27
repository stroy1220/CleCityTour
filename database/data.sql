-- *****************************************************************************
-- This script contains INSERT statements for populating tables with seed data
-- *****************************************************************************
--DROP TABLE itinerary;
--DROP TABLE itineraryPlaces;
--DROP TABLE places;
--DROP TABLE userInfo;

BEGIN;
-- INSERT statements go here

COMMIT;


insert into places(streetAddress, city, state, latitude, longitude, detail, placeName, Category, Zip) values('1 Center Ct', 'Cleveland', 'OH', 41.496873,  -81.689694,'Large indoor venue for pro sports such as hockey, basketball & arena football plus various concerts','Quicken Loans Arena','Sports', 44113)

insert into places(streetAddress, city, state, latitude, longitude, detail, placeName, Category, Zip) values('2401 Ontario St', 'Cleveland', 'OH', 41.494517, -81.685398, 'Cleveland Indians home field with a mezzanine-level kid''s club, concessions & youth workshops', 'Progressive Field','Sports', 44113)

insert into places(streetAddress, city, state, latitude, longitude, detail, placeName, Category, Zip) values('100 Alfred Lerner Way', 'Cleveland', 'OH', 41.505097,  -81.698764, '68,000-seat lakefront football stadium hosting the NFL''s Cleveland Browns, concerts & events', 'FirstEnergy Stadium', 'Sports', 44113)

insert into places(streetAddress, city, state, latitude, longitude, detail, placeName, Category, Zip) values('2050 E 4th St', 'Cleveland', 'OH', 41.499374, -81.690238, 'Cool, bi-level, industrial-chic BBQ spot with communal tables & a long bar with many bourbons', 'Mabel''s BBQ' ,'Restaurant', 44113)

insert into places(streetAddress, city, state, latitude, longitude, detail, placeName, Category, Zip) values('347 Euclid Ave', 'Cleveland', 'OH', 41.499673, -81.691234, 'Trendy dessert & martini bar with a lineup of sandwiches, flatbreads, pasta & entrees', 'Chocolate Bar','Restaurant', 44113)

insert into places(streetAddress, city, state, latitude, longitude, detail, placeName, Category, Zip) values('417 Prospect Ave E', 'Cleveland', 'OH', 41.498322,  -81.689789,'Sleek, modern outpost of a small chain specializing in traditional chophouse fare, pasta & seafood','Red, the Steakhouse','Restaurant', 44113)

insert into places(streetAddress, city, state, latitude, longitude, detail, placeName, Category, Zip) values('700 W St Clair Ave', 'Cleveland', 'OH', 41.499348,  -81.699057,'Bustling institution serving upscale seafood in an elegant, multilevel setting with skyline views','Blue Point Grill','Restaurant', 44113)

insert into places(streetAddress, city, state, latitude, longitude, detail, placeName, Category, Zip) values('1100 E 9th St', 'Cleveland', 'OH', 41.506486, -81.692348, 'Iconic music-minded museum loaded with pop-culture memorabilia, artifacts, instruments & a jukebox', 'Rock & Roll Hall of Fame', 'Entertainment', 44113)

insert into places(streetAddress, city, state, latitude, longitude, detail, placeName, Category, Zip) values('1231 Main Ave', 'Cleveland', 'OH', 41.49581, -81.706796, 'Seasonal, 5,000-seat waterfront concert venue set within a dining & entertainment complex', 'Nautica Entertainment Complex', 'Entertainment', 44113)

insert into places(streetAddress, city, state, latitude, longitude, detail, placeName, Category, Zip) values('308 Euclid Ave', 'Cleveland', 'OH', 41.499628, -81.690876, 'Rock- & blues-themed chain with Southern dishes such as po-boys & jambalaya, plus live music', 'House of Blues', 'Entertainment', 44113)

insert into places(streetAddress, city, state, latitude, longitude, detail, placeName, Category, Zip) values('1501 Euclid Ave #200','Cleveland', 'OH', 41.50104, -81.681023, 'Performing arts center for Broadway-style shows, concerts, dance productions, plays, comedy & opera','Playhouse Square','Entertainment', 44113)

insert into places(streetAddress, city, state, latitude, longitude, detail, placeName, Category, Zip) values('4308 Franklin Blvd', 'Cleveland', 'OH',  41.485458, -81.716506, 'Grand, historic mansion, built in the 19th century, featuring ornate Victorian details','Franklin Castle','Landmark', 44113)

insert into places(streetAddress, city, state, latitude, longitude, detail, placeName, Category, Zip) values('11150 East Blvd', 'Cleveland', 'OH', 41.506757, -81.610276, 'University Circle is a district in the neighborhood of University on the south east side of Cleveland, Ohio', 'University Circle' ,'Landmark', 44113)

insert into places(streetAddress, city, state, latitude, longitude, detail, placeName, Category, Zip) values('3159 W 11th St', 'Cleveland', 'OH', 41.468755, -81.687625, 'House used in "A Christmas Story" film, now a museum with a replicated interior & film memorabilia', 'A Christmas Story House', 'Landmark', 44113)

insert into places(streetAddress, city, state, latitude, longitude, detail, placeName, Category, Zip) values('36 W Superior Ave', 'Cleveland', 'OH', 41.499596, -81.693777, 'Public Square is the four-block central plaza of downtown Cleveland, Ohio. Based on an 18th-century New England model, it was part of the original 1796 town plat overseen by Moses Cleaveland','Public Square', 'Landmark', 44113)

insert into places(streetAddress, city, state, latitude, longitude, detail, placeName, Category, Zip) values('1301 E 9th St', 'Cleveland', 'OH',  41.504211, -81.690306, 'Bi-level, atrium-style venue featuring stores & services, plus restaurants & a food court', 'Galleria at Erieview', 'Shopping', 44113)

insert into places(streetAddress, city, state, latitude, longitude, detail, placeName, Category, Zip) values('3447 Steelyard Dr', 'Cleveland', 'OH', 41.460639, -81.690555,'This sizable shopping center features familiar big-box stores, along with counter-serve eateries.', 'Steelyard Commons', 'Shopping', 44113)

insert into places(streetAddress, city, state, latitude, longitude, detail, placeName, Category, Zip) values('401 Euclid Ave', 'Cleveland', 'OH', 41.499746, -81.690662, 'Soaring, ornate indoor mall, opened in 1890, today housing the Hyatt hotel, shops & restaurants', 'The Arcade', 'Shopping', 44113)

insert into places(streetAddress, city, state, latitude, longitude, detail, placeName, Category, Zip) values('230 W Huron Rd', 'Cleveland', 'OH', 41.496762, -81.694051, 'Tower City Center originally known as Cleveland Union Terminal is located on Public Square. Year built:1927 Area:15 acres', 'Tower City Center', 'Shopping', 44113)

 insert into itinerary values(1, 'fakename', '01/01/01', '41.496873', '-81.689694'); 
 insert into itinerary values(1, 'fakename2', '01/01/01','41.496873', '-81.689694');
 insert into itinerary values(2, 'fakename', '01/01/01', '41.499374', '-81.690238'); 
 insert into itinerary values(2, 'fakename2', '01/01/01', '41.499374', '-81.690238');

 insert into itineraryPlaces values(1, 1)
 insert into itineraryPlaces values(1, 2)
 insert into itineraryPlaces values(1, 3)

 insert into itineraryPlaces values(2, 4)
 insert into itineraryPlaces values(2, 5)
 insert into itineraryPlaces values(2, 6)

delete from itineraryPlaces where itineraryID = 1 delete from itinerary where id = 1
