

GRANT ALL PRIVILEGES ON DATABASE medicaredb TO dbuser;


CREATE TABLE facilities (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    city VARCHAR(100) NOT NULL
);


CREATE TABLE patients (
    id SERIAL PRIMARY KEY,
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    age INT NOT NULL
);


CREATE TABLE payers (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    city VARCHAR(100) NOT NULL
);


CREATE TABLE encounters (
    id SERIAL PRIMARY KEY,
    patient_id INT NOT NULL,
    facility_id INT NOT NULL,
    payer_id INT NOT NULL,
    visit_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (patient_id) REFERENCES patients (id),
    FOREIGN KEY (facility_id) REFERENCES facilities (id),
    FOREIGN KEY (payer_id) REFERENCES payers (id)
);

CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    username VARCHAR(100) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);


INSERT INTO facilities (name, city) VALUES
('Hospital Central', 'Bogota'),
('Clinica del Norte', 'Medellin'),
('Clinica del Sur', 'Cali');


INSERT INTO patients (first_name, last_name, age) VALUES
('Juan', 'Perez', 30),
('Maria', 'Garcia', 25),
('Luis', 'Martinez', 15);


INSERT INTO payers (name, city) VALUES
('Seguro Salud S.A.', 'Bogota'),
('Cobertura Total', 'Medellin'),
('Proteccion Familiar', 'Cali');


INSERT INTO encounters (patient_id, facility_id, payer_id, visit_date) VALUES
(1, 1, 1, '2024-05-01 10:00:00'), 
(1, 2, 2, '2024-05-03 12:00:00'),
(2, 1, 1, '2024-05-02 11:00:00'),
(2, 3, 3, '2024-05-04 09:00:00'),
(3, 2, 2, '2024-05-05 14:00:00'), 
(3, 3, 3, '2024-05-06 16:00:00');
