-- *****************************************************************************
-- This script contains INSERT statements for populating tables with seed data
-- *****************************************************************************

BEGIN;

-- INSERT statements go here

insert into userInfo (userName, firstName, lastName, password, admin) values('p.r@gmail.com', 'Pete', 'Rader', 'F0AE7F9D0BD3F50A1F9057895B0B5847', 0);
COMMIT;

select * from userInfo;