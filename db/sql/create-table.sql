DROP TABLE IF EXISTS cars;
CREATE TABLE cars(id SERIAL PRIMARY KEY, name VARCHAR(255), price INT);

INSERT INTO cars(name, price) VALUES('Audi',52642);
INSERT INTO cars(name, price) VALUES('Mercedes',57127);
INSERT INTO cars(name, price) VALUES('Skoda',9000);