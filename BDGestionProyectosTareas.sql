use master
go

if not exists (select[name] from sys.databases where [name]=N'GestionProyectosTareas')
begin
	create database GestionProyectosTareas
end;

use GestionProyectosTareas;
go

CREATE TABLE Empleado (
	cedula int PRIMARY KEY,
	nombre varchar(15),
	apellido1 varchar(15),
	apellido2 varchar(15),
	telefono int,
)

CREATE TABLE Departamento (
	codigo int PRIMARY KEY,
	nombre varchar(30),
	cedula_jefe int,
	FOREIGN KEY (cedula_jefe) REFERENCES Empleado(cedula)
)

CREATE TABLE Proyecto (
	nombre_proyecto varchar(20) PRIMARY KEY,
	nombre_portafolio varchar(20),
	descripcion varchar(255),
	tipo varchar(20),
	año int,
	trimestre tinyint,
	fecha_inicio date,
	fecha_cierre date,
	codigoDep int,
	FOREIGN KEY(codigoDep) REFERENCES Departamento(codigo)
)

CREATE TABLE EmpleadoProyecto(
	cedula_empleado int,
	nombre_proyecto varchar(20),
	PRIMARY KEY(cedula_empleado, nombre_proyecto),
	FOREIGN KEY (cedula_empleado) REFERENCES Empleado(cedula),
	FOREIGN KEY (nombre_proyecto) REFERENCES Proyecto(nombre_proyecto)
)

CREATE TABLE Tarea (
	nombre_tarea varchar(20) PRIMARY KEY,
	tipo varchar(20),
	descripcion varchar(255),
	cantidad_horas int,
	nombre_proyecto varchar(20),
	FOREIGN KEY (nombre_proyecto) REFERENCES Proyecto(nombre_proyecto)
)

CREATE TABLE EmpleadoTarea (
	cedula_empleado int,
	nombre_tarea varchar(20),
	PRIMARY KEY (cedula_empleado,nombre_tarea),
	FOREIGN KEY (cedula_empleado) REFERENCES Empleado(cedula),
	FOREIGN KEY (nombre_tarea) REFERENCES Tarea(nombre_tarea)
)

CREATE TABLE Actividad (
	fecha_hora_inicio datetime,
	fecha_hora_final datetime,
	horas int,
	tipo varchar(15),
	etapa varchar(15),
	nombre_tarea varchar(20),
	cedula_empleado int,
	PRIMARY KEY (fecha_hora_inicio, nombre_tarea, cedula_empleado),
	FOREIGN KEY (nombre_tarea) REFERENCES Tarea(nombre_tarea),
	FOREIGN KEY (cedula_empleado) REFERENCES Empleado(cedula)
)

INSERT INTO Empleado (cedula, nombre, apellido1, apellido2, telefono) VALUES
(101, 'Carlos', 'Gomez', 'Perez', 5551234),
(102, 'Maria', 'Lopez', 'Rodriguez', 5555678),
(103, 'Juan', 'Martinez', 'Garcia', 5559876),
(104, 'Ana', 'Hernandez', 'Sanchez', 5556543),
(105, 'Luis', 'Ramirez', 'Torres', 5557890);

INSERT INTO Departamento (codigo, nombre, cedula_jefe) VALUES
(201, 'Desarrollo', 101),
(202, 'Marketing', 102),
(203, 'Finanzas', 103),
(204, 'Recursos Humanos', 104),
(205, 'Operaciones', 105);

INSERT INTO Proyecto (nombre_proyecto, nombre_portafolio, descripcion, tipo, año, trimestre, fecha_inicio, fecha_cierre, codigoDep) VALUES
('ProyectoA', 'Portafolio1', 'Desarrollo de software A', 'Desarrollo', 2024, 1, '2024-01-10', '2024-06-30', 201),
('ProyectoB', 'Portafolio2', 'Campaña de marketing B', 'Marketing', 2024, 2, '2024-04-01', '2024-09-30', 202),
('ProyectoC', 'Portafolio3', 'Auditoría financiera C', 'Finanzas', 2024, 3, '2024-07-15', '2024-12-31', 203),
('ProyectoD', 'Portafolio4', 'Reclutamiento de personal D', 'Recursos Humanos', 2024, 4, '2024-09-01', '2025-02-28', 204),
('ProyectoE', 'Portafolio5', 'Optimización de procesos E', 'Operaciones', 2024, 1, '2024-02-15', '2024-08-15', 205);

INSERT INTO EmpleadoProyecto (cedula_empleado, nombre_proyecto) VALUES
(101, 'ProyectoA'),
(102, 'ProyectoB'),
(103, 'ProyectoC'),
(104, 'ProyectoD'),
(105, 'ProyectoE');

INSERT INTO Tarea (nombre_tarea, tipo, descripcion, cantidad_horas, nombre_proyecto) VALUES
('Tarea1', 'Desarrollo', 'Implementación de módulo 1', 40, 'ProyectoA'),
('Tarea2', 'Marketing', 'Diseño de campaña', 30, 'ProyectoB'),
('Tarea3', 'Finanzas', 'Revisión de cuentas', 25, 'ProyectoC'),
('Tarea4', 'RRHH', 'Entrevistas de candidatos', 20, 'ProyectoD'),
('Tarea5', 'Operaciones', 'Mejora de procesos', 35, 'ProyectoE');

INSERT INTO EmpleadoTarea (cedula_empleado, nombre_tarea) VALUES
(101, 'Tarea1'),
(102, 'Tarea2'),
(103, 'Tarea3'),
(104, 'Tarea4'),
(105, 'Tarea5');

INSERT INTO Actividad (fecha_hora_inicio, fecha_hora_final, horas, tipo, etapa, nombre_tarea, cedula_empleado) VALUES
('2024-01-12 09:00:00', '2024-01-12 13:00:00', 4, 'Desarrollo', 'testing', 'Tarea1', 101),
('2024-04-05 10:00:00', '2024-04-05 14:00:00', 4, 'Marketing', 'revision', 'Tarea2', 102),
('2024-07-20 09:30:00', '2024-07-20 12:30:00', 3, 'Finanzas', 'finalizada', 'Tarea3', 103),
('2024-09-03 08:00:00', '2024-09-03 11:00:00', 3, 'RRHH', 'testing', 'Tarea4', 104),
('2024-02-20 14:00:00', '2024-02-20 17:00:00', 3, 'Operaciones', 'revision', 'Tarea5', 105);
