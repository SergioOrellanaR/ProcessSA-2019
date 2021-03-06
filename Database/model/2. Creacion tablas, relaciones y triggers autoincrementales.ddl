-- Generado por Oracle SQL Developer Data Modeler 19.2.0.182.1216
--   en:        2019-08-26 08:11:50 GMT-04:00
--   sitio:      Oracle Database 11g
--   tipo:      Oracle Database 11g



CREATE SEQUENCE assigned_unit_id_seq;

CREATE SEQUENCE commune_id_seq;

CREATE SEQUENCE company_id_seq;

CREATE SEQUENCE day_of_week_id_seq;

CREATE SEQUENCE gender_id_seq;

CREATE SEQUENCE internal_unit_id_seq;

CREATE SEQUENCE loop_task_id_seq;

CREATE SEQUENCE loop_task_schedule_id_seq;

CREATE SEQUENCE month_id_seq;

CREATE SEQUENCE permission_id_seq;

CREATE SEQUENCE processed_task_id_seq;

CREATE SEQUENCE province_id_seq;

CREATE SEQUENCE pt_history_id_seq;

CREATE SEQUENCE region_id_seq;

CREATE SEQUENCE role_id_seq;

CREATE SEQUENCE task_assignment_id_seq;

CREATE SEQUENCE task_id_seq;

CREATE SEQUENCE task_status_id_seq;

CREATE SEQUENCE user_id_seq;

CREATE SEQUENCE working_area_id_seq;

CREATE TABLE assigned_unit (
    id                 INTEGER NOT NULL,
    id_internalunit    INTEGER NOT NULL,
    id_company         INTEGER NOT NULL,
    id_superior_unit   INTEGER
);

ALTER TABLE assigned_unit ADD CONSTRAINT assigned_unit_pk PRIMARY KEY ( id );

CREATE TABLE commune (
    id            INTEGER NOT NULL,
    name          VARCHAR2(255) NOT NULL,
    id_province   INTEGER NOT NULL
);

ALTER TABLE commune ADD CONSTRAINT commune_pk PRIMARY KEY ( id );

CREATE TABLE company (
    id                INTEGER NOT NULL,
    name              VARCHAR2(255) NOT NULL,
    address           NVARCHAR2(255) NOT NULL,
    logo              BLOB,
    id_working_area   INTEGER NOT NULL,
    id_commune        INTEGER NOT NULL
);

ALTER TABLE company ADD CONSTRAINT company_pk PRIMARY KEY ( id );

CREATE TABLE day_of_week (
    id     INTEGER NOT NULL,
    name   NVARCHAR2(12) NOT NULL
);

ALTER TABLE day_of_week ADD CONSTRAINT day_of_week_pk PRIMARY KEY ( id );

CREATE TABLE gender (
    id     INTEGER NOT NULL,
    name   VARCHAR2(20) NOT NULL
);

ALTER TABLE gender ADD CONSTRAINT gender_pk PRIMARY KEY ( id );

CREATE TABLE internal_unit (
    id        INTEGER NOT NULL,
    name      NVARCHAR2(255) NOT NULL,
    id_role   INTEGER NOT NULL
);

ALTER TABLE internal_unit ADD CONSTRAINT internal_unit_pk PRIMARY KEY ( id );

CREATE TABLE loop_task (
    id                   INTEGER NOT NULL,
    id_task_assignment   INTEGER NOT NULL,
    time                 DATE NOT NULL,
    isactive             CHAR(1) NOT NULL
);

ALTER TABLE loop_task ADD CONSTRAINT loop_task_pk PRIMARY KEY ( id );

CREATE TABLE loop_task_schedule (
    id             INTEGER NOT NULL,
    id_loop_task   INTEGER NOT NULL,
    id_dayofweek   INTEGER,
    dayofmonth     INTEGER,
    numberofweek   INTEGER,
    id_month       INTEGER
);

ALTER TABLE loop_task_schedule ADD CONSTRAINT loop_task_schedule_pk PRIMARY KEY ( id );

CREATE TABLE month (
    id     INTEGER NOT NULL,
    name   NVARCHAR2(12) NOT NULL
);

ALTER TABLE month ADD CONSTRAINT month_pk PRIMARY KEY ( id );

CREATE TABLE permission (
    id            INTEGER NOT NULL,
    name          NVARCHAR2(250) NOT NULL,
    description   NVARCHAR2(500)
);

ALTER TABLE permission ADD CONSTRAINT permissions_pk PRIMARY KEY ( id );

CREATE TABLE processed_task (
    id                      INTEGER NOT NULL,
    commentary              NVARCHAR2(1000),
    assignationdate         DATE NOT NULL,
    enddate                 DATE NOT NULL,
    id_taskstatus           INTEGER NOT NULL,
    id_task_assignment      INTEGER NOT NULL,
    id_scheduled_looptask   INTEGER
);

CREATE UNIQUE INDEX processed_task__idx ON
    processed_task (
        id_scheduled_looptask
    ASC );

ALTER TABLE processed_task ADD CONSTRAINT assigned_task_pk PRIMARY KEY ( id );

CREATE TABLE processed_task_history (
    id                    INTEGER NOT NULL,
    id_assignedtask       INTEGER NOT NULL,
    description           NVARCHAR2(1000) NOT NULL,
    eventdatetime         DATE NOT NULL,
    justification         NVARCHAR2(1000),
    id_userjustificator   INTEGER
);

ALTER TABLE processed_task_history ADD CONSTRAINT atask_change_history_pk PRIMARY KEY ( id );

CREATE TABLE province (
    id          INTEGER NOT NULL,
    name        NVARCHAR2(255) NOT NULL,
    id_region   INTEGER NOT NULL
);

ALTER TABLE province ADD CONSTRAINT province_pk PRIMARY KEY ( id );

CREATE TABLE region (
    id     INTEGER NOT NULL,
    name   NVARCHAR2(255) NOT NULL
);

ALTER TABLE region ADD CONSTRAINT region_pk PRIMARY KEY ( id );

CREATE TABLE role (
    id     INTEGER NOT NULL,
    name   NVARCHAR2(64) NOT NULL
);

ALTER TABLE role ADD CONSTRAINT role_pk PRIMARY KEY ( id );

CREATE TABLE role_permissions (
    id_role         INTEGER NOT NULL,
    id_permission   INTEGER NOT NULL
);

ALTER TABLE role_permissions ADD CONSTRAINT role_permissions_pk PRIMARY KEY ( id_role,
                                                                              id_permission );

CREATE TABLE task (
    id                  INTEGER NOT NULL,
    name                NVARCHAR2(255) NOT NULL,
    description         NVARCHAR2(1000) NOT NULL,
    ispredefinided      CHAR(1) NOT NULL,
    id_superior_task    INTEGER,
    id_dependent_task   INTEGER
);

ALTER TABLE task ADD CONSTRAINT tasks_pk PRIMARY KEY ( id );

CREATE TABLE task_assignment (
    id                INTEGER NOT NULL,
    id_task           INTEGER NOT NULL,
    id_assigneruser   INTEGER NOT NULL,
    id_receiveruser   INTEGER
);

ALTER TABLE task_assignment ADD CONSTRAINT task_assignment_pk PRIMARY KEY ( id );

CREATE TABLE task_status (
    id       INTEGER NOT NULL,
    status   NVARCHAR2(255) NOT NULL
);

ALTER TABLE task_status ADD CONSTRAINT task_status_pk PRIMARY KEY ( id );

CREATE TABLE user_info (
    id                 INTEGER NOT NULL,
    firstname          NVARCHAR2(255) NOT NULL,
    lastname           NVARCHAR2(255) NOT NULL,
    address            NVARCHAR2(255) NOT NULL,
    phone              VARCHAR2(20) NOT NULL,
    birthdate          DATE NOT NULL,
    email              NVARCHAR2(255) NOT NULL,
    password           NVARCHAR2(255) NOT NULL,
    id_commune         INTEGER NOT NULL,
    id_assigned_unit   INTEGER NOT NULL,
    id_company         INTEGER,
    id_gender          INTEGER NOT NULL
);

CREATE UNIQUE INDEX email__idx ON
    user_info (
        email
    ASC );

ALTER TABLE user_info ADD CONSTRAINT user_pk PRIMARY KEY ( id );

CREATE TABLE working_area (
    id     INTEGER NOT NULL,
    name   NVARCHAR2(255) NOT NULL
);

ALTER TABLE working_area ADD CONSTRAINT working_area_pk PRIMARY KEY ( id );

ALTER TABLE processed_task
    ADD CONSTRAINT assigned_task_proc_task_fk FOREIGN KEY ( id_task_assignment )
        REFERENCES task_assignment ( id )
    NOT DEFERRABLE;

ALTER TABLE processed_task
    ADD CONSTRAINT assigned_task_task_fk FOREIGN KEY ( id_taskstatus )
        REFERENCES task_status ( id )
    NOT DEFERRABLE;

ALTER TABLE assigned_unit
    ADD CONSTRAINT assigned_unit_assigned_role_fk FOREIGN KEY ( id_superior_unit )
        REFERENCES assigned_unit ( id )
    NOT DEFERRABLE;

ALTER TABLE assigned_unit
    ADD CONSTRAINT assigned_unit_company_fk FOREIGN KEY ( id_company )
        REFERENCES company ( id )
    NOT DEFERRABLE;

ALTER TABLE assigned_unit
    ADD CONSTRAINT assigned_unit_role_fk FOREIGN KEY ( id_internalunit )
        REFERENCES internal_unit ( id )
    NOT DEFERRABLE;

ALTER TABLE processed_task_history
    ADD CONSTRAINT atask_change_history_user_fk FOREIGN KEY ( id_userjustificator )
        REFERENCES user_info ( id )
    NOT DEFERRABLE;

ALTER TABLE commune
    ADD CONSTRAINT commune_province_fk FOREIGN KEY ( id_province )
        REFERENCES province ( id )
    NOT DEFERRABLE;

ALTER TABLE company
    ADD CONSTRAINT company_commune_fk FOREIGN KEY ( id_commune )
        REFERENCES commune ( id )
    NOT DEFERRABLE;

ALTER TABLE company
    ADD CONSTRAINT company_working_area_fk FOREIGN KEY ( id_working_area )
        REFERENCES working_area ( id )
    NOT DEFERRABLE;

ALTER TABLE province
    ADD CONSTRAINT id_region_fk FOREIGN KEY ( id_region )
        REFERENCES region ( id )
    NOT DEFERRABLE;

ALTER TABLE loop_task
    ADD CONSTRAINT loop_task_month_fk FOREIGN KEY ( id_task_assignment )
        REFERENCES task_assignment ( id )
    NOT DEFERRABLE;

ALTER TABLE loop_task_schedule
    ADD CONSTRAINT lt_schedule_day_of_week_fk FOREIGN KEY ( id_dayofweek )
        REFERENCES day_of_week ( id )
    NOT DEFERRABLE;

ALTER TABLE loop_task_schedule
    ADD CONSTRAINT lt_schedule_loop_task_fk FOREIGN KEY ( id_loop_task )
        REFERENCES loop_task ( id )
    NOT DEFERRABLE;

ALTER TABLE loop_task_schedule
    ADD CONSTRAINT lt_schedule_month_fk FOREIGN KEY ( id_month )
        REFERENCES month ( id )
    NOT DEFERRABLE;

ALTER TABLE processed_task
    ADD CONSTRAINT processed_task_lt_schedule_fk FOREIGN KEY ( id_scheduled_looptask )
        REFERENCES loop_task_schedule ( id )
    NOT DEFERRABLE;

ALTER TABLE internal_unit
    ADD CONSTRAINT role_internalunit_fk FOREIGN KEY ( id_role )
        REFERENCES role ( id )
    NOT DEFERRABLE;

ALTER TABLE role_permissions
    ADD CONSTRAINT rtype_permission_p_fk FOREIGN KEY ( id_permission )
        REFERENCES permission ( id )
    NOT DEFERRABLE;

ALTER TABLE role_permissions
    ADD CONSTRAINT rtype_permission_rt_fk FOREIGN KEY ( id_role )
        REFERENCES role ( id )
    NOT DEFERRABLE;

ALTER TABLE processed_task_history
    ADD CONSTRAINT table_18_assigned_task_fk FOREIGN KEY ( id_assignedtask )
        REFERENCES processed_task ( id )
    NOT DEFERRABLE;

ALTER TABLE task_assignment
    ADD CONSTRAINT task_assignment_fk FOREIGN KEY ( id_task )
        REFERENCES task ( id )
    NOT DEFERRABLE;

ALTER TABLE task_assignment
    ADD CONSTRAINT task_assignment_user_fk FOREIGN KEY ( id_assigneruser )
        REFERENCES user_info ( id )
    NOT DEFERRABLE;

ALTER TABLE task_assignment
    ADD CONSTRAINT task_receiver_user_fk FOREIGN KEY ( id_receiveruser )
        REFERENCES user_info ( id )
    NOT DEFERRABLE;

ALTER TABLE task
    ADD CONSTRAINT task_task_fk FOREIGN KEY ( id_superior_task )
        REFERENCES task ( id )
    NOT DEFERRABLE;

ALTER TABLE task
    ADD CONSTRAINT task_task_fkv1 FOREIGN KEY ( id_dependent_task )
        REFERENCES task ( id )
    NOT DEFERRABLE;

ALTER TABLE user_info
    ADD CONSTRAINT user_assigned_role_fk FOREIGN KEY ( id_assigned_unit )
        REFERENCES assigned_unit ( id )
    NOT DEFERRABLE;

ALTER TABLE user_info
    ADD CONSTRAINT user_commune_fk FOREIGN KEY ( id_commune )
        REFERENCES commune ( id )
    NOT DEFERRABLE;

ALTER TABLE user_info
    ADD CONSTRAINT user_company_fk FOREIGN KEY ( id_company )
        REFERENCES company ( id )
    NOT DEFERRABLE;

ALTER TABLE user_info
    ADD CONSTRAINT user_gender_fk FOREIGN KEY ( id_gender )
        REFERENCES gender ( id )
    NOT DEFERRABLE;

CREATE OR REPLACE TRIGGER assigned_unit_ai 
    BEFORE INSERT ON assigned_unit 
    FOR EACH ROW 
BEGIN
  SELECT assigned_unit_id_seq.nextval
  INTO :new.id
  FROM dual;
END; 
/

CREATE OR REPLACE TRIGGER commune_ai 
    BEFORE INSERT ON commune 
    FOR EACH ROW 
BEGIN
  SELECT commune_id_seq.nextval
  INTO :new.id
  FROM dual;
END; 
/

CREATE OR REPLACE TRIGGER company_ai 
    BEFORE INSERT ON company 
    FOR EACH ROW 
BEGIN
  SELECT company_id_seq.nextval
  INTO :new.id
  FROM dual;
END; 
/

CREATE OR REPLACE TRIGGER day_of_week_ai 
    BEFORE INSERT ON day_of_week 
    FOR EACH ROW 
BEGIN
  SELECT day_of_week_id_seq.nextval
  INTO :new.id
  FROM dual;
END; 
/

CREATE OR REPLACE TRIGGER gender_ai 
    BEFORE INSERT ON gender 
    FOR EACH ROW 
BEGIN
  SELECT gender_id_seq.nextval
  INTO :new.id
  FROM dual;
END; 
/

CREATE OR REPLACE TRIGGER internal_unit_ai 
    BEFORE INSERT ON internal_unit 
    FOR EACH ROW 
BEGIN
  SELECT internal_unit_id_seq.nextval
  INTO :new.id
  FROM dual;
END; 
/

CREATE OR REPLACE TRIGGER loop_task_ai 
    BEFORE INSERT ON loop_task 
    FOR EACH ROW 
BEGIN
  SELECT loop_task_id_seq.nextval
  INTO :new.id
  FROM dual;
END; 
/

CREATE OR REPLACE TRIGGER loop_task_schedule_ai 
    BEFORE INSERT ON loop_task_schedule 
    FOR EACH ROW 
BEGIN
  SELECT loop_task_schedule_id_seq.nextval
  INTO :new.id
  FROM dual;
END; 
/

CREATE OR REPLACE TRIGGER month_ai 
    BEFORE INSERT ON month 
    FOR EACH ROW 
BEGIN
  SELECT month_id_seq.nextval
  INTO :new.id
  FROM dual;
END; 
/

CREATE OR REPLACE TRIGGER permission_ai 
    BEFORE INSERT ON permission 
    FOR EACH ROW 
BEGIN
  SELECT permission_id_seq.nextval
  INTO :new.id
  FROM dual;
END; 
/

CREATE OR REPLACE TRIGGER processed_task_ai 
    BEFORE INSERT ON processed_task 
    FOR EACH ROW 
BEGIN
  SELECT processed_task_id_seq.nextval
  INTO :new.id
  FROM dual;
END; 
/

CREATE OR REPLACE TRIGGER processed_task_history_ai 
    BEFORE INSERT ON processed_task_history 
    FOR EACH ROW 
BEGIN
  SELECT pt_history_id_seq.nextval
  INTO :new.id
  FROM dual;
END; 
/

CREATE OR REPLACE TRIGGER province_ai 
    BEFORE INSERT ON province 
    FOR EACH ROW 
BEGIN
  SELECT province_id_seq.nextval
  INTO :new.id
  FROM dual;
END; 
/

CREATE OR REPLACE TRIGGER region_ai 
    BEFORE INSERT ON region 
    FOR EACH ROW 
BEGIN
  SELECT region_id_seq.nextval
  INTO :new.id
  FROM dual;
END; 
/

CREATE OR REPLACE TRIGGER role_ai 
    BEFORE INSERT ON role 
    FOR EACH ROW 
BEGIN
  SELECT role_id_seq.nextval
  INTO :new.id
  FROM dual;
END; 
/

CREATE OR REPLACE TRIGGER task_ai 
    BEFORE INSERT ON task 
    FOR EACH ROW 
BEGIN
  SELECT task_id_seq.nextval
  INTO :new.id
  FROM dual;
END; 
/

CREATE OR REPLACE TRIGGER task_assignment_ai 
    BEFORE INSERT ON task_assignment 
    FOR EACH ROW 
BEGIN
  SELECT task_assignment_id_seq.nextval
  INTO :new.id
  FROM dual;
END; 
/

CREATE OR REPLACE TRIGGER task_status_ai 
    BEFORE INSERT ON task_status 
    FOR EACH ROW 
BEGIN
  SELECT task_status_id_seq.nextval
  INTO :new.id
  FROM dual;
END; 
/

CREATE OR REPLACE TRIGGER user_ai 
    BEFORE INSERT ON user_info 
    FOR EACH ROW 
BEGIN
  SELECT user_id_seq.nextval
  INTO :new.id
  FROM dual;
END; 
/

CREATE OR REPLACE TRIGGER working_area_ai 
    BEFORE INSERT ON working_area 
    FOR EACH ROW 
BEGIN
  SELECT working_area_id_seq.nextval
  INTO :new.id
  FROM dual;
END; 
/



-- Informe de Resumen de Oracle SQL Developer Data Modeler: 
-- 
-- CREATE TABLE                            21
-- CREATE INDEX                             2
-- ALTER TABLE                             49
-- CREATE VIEW                              0
-- ALTER VIEW                               0
-- CREATE PACKAGE                           0
-- CREATE PACKAGE BODY                      0
-- CREATE PROCEDURE                         0
-- CREATE FUNCTION                          0
-- CREATE TRIGGER                          20
-- ALTER TRIGGER                            0
-- CREATE COLLECTION TYPE                   0
-- CREATE STRUCTURED TYPE                   0
-- CREATE STRUCTURED TYPE BODY              0
-- CREATE CLUSTER                           0
-- CREATE CONTEXT                           0
-- CREATE DATABASE                          0
-- CREATE DIMENSION                         0
-- CREATE DIRECTORY                         0
-- CREATE DISK GROUP                        0
-- CREATE ROLE                              0
-- CREATE ROLLBACK SEGMENT                  0
-- CREATE SEQUENCE                         20
-- CREATE MATERIALIZED VIEW                 0
-- CREATE MATERIALIZED VIEW LOG             0
-- CREATE SYNONYM                           0
-- CREATE TABLESPACE                        0
-- CREATE USER                              0
-- 
-- DROP TABLESPACE                          0
-- DROP DATABASE                            0
-- 
-- REDACTION POLICY                         0
-- 
-- ORDS DROP SCHEMA                         0
-- ORDS ENABLE SCHEMA                       0
-- ORDS ENABLE OBJECT                       0
-- 
-- ERRORS                                   0
-- WARNINGS                                 0
