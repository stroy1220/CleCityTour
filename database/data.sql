-- *****************************************************************************
-- This script contains INSERT statements for populating tables with seed data
-- *****************************************************************************

BEGIN;

-- INSERT statements go here

insert into userInfo (userName, firstName, lastName, password, admin) values('p.r@gmail.com', 'Pete', 'Rader', 'F0AE7F9D0BD3F50A1F9057895B0B5847', 0);
COMMIT;

select * from places;
delete from places where id = 2
alter table places alter column latitude varchar(max) not null

update places set longitude = -81.699057 where id =18

insert into places(streetAddress, city, state, latitude, longitude, detail, placeName, Category) values('1 Center Ct', 'Cleveland', 'OH', 41.496873,  -81.689694,'Large indoor venue for pro sports such as hockey, basketball & arena football plus various concerts','Quicken Loans Arena','Sports')