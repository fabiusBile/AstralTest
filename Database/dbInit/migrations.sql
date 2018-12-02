CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);


DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20181130214212_CreateAll') THEN
    CREATE TABLE "Employers" (
        "Id" text NOT NULL,
        "Name" character varying(255) NULL,
        CONSTRAINT "PK_Employers" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20181130214212_CreateAll') THEN
    CREATE TABLE "EmploymentType" (
        "Id" character varying(63) NOT NULL,
        "Name" character varying(63) NULL,
        CONSTRAINT "PK_EmploymentType" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20181130214212_CreateAll') THEN
    CREATE TABLE "Vacancies" (
        "Id" text NOT NULL,
        "Name" character varying(255) NULL,
        "Salary" integer NULL,
        "ContactPerson" character varying(63) NULL,
        "PhoneNumber" character varying(12) NULL,
        "Description" character varying(1000) NULL,
        "EmployerId" text NULL,
        "TypeId" character varying(63) NULL,
        CONSTRAINT "PK_Vacancies" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Vacancies_Employers_EmployerId" FOREIGN KEY ("EmployerId") REFERENCES "Employers" ("Id") ON DELETE RESTRICT,
        CONSTRAINT "FK_Vacancies_EmploymentType_TypeId" FOREIGN KEY ("TypeId") REFERENCES "EmploymentType" ("Id") ON DELETE RESTRICT
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20181130214212_CreateAll') THEN
    CREATE INDEX "IX_Vacancies_EmployerId" ON "Vacancies" ("EmployerId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20181130214212_CreateAll') THEN
    CREATE INDEX "IX_Vacancies_TypeId" ON "Vacancies" ("TypeId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20181130214212_CreateAll') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20181130214212_CreateAll', '2.1.4-rtm-31024');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20181130220528_AddEmploymentType') THEN
    ALTER TABLE "Vacancies" DROP CONSTRAINT "FK_Vacancies_EmploymentType_TypeId";
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20181130220528_AddEmploymentType') THEN
    ALTER TABLE "EmploymentType" DROP CONSTRAINT "PK_EmploymentType";
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20181130220528_AddEmploymentType') THEN
    ALTER TABLE "EmploymentType" RENAME TO "EmploymentTypes";
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20181130220528_AddEmploymentType') THEN
    ALTER TABLE "EmploymentTypes" ADD CONSTRAINT "PK_EmploymentTypes" PRIMARY KEY ("Id");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20181130220528_AddEmploymentType') THEN
    ALTER TABLE "Vacancies" ADD CONSTRAINT "FK_Vacancies_EmploymentTypes_TypeId" FOREIGN KEY ("TypeId") REFERENCES "EmploymentTypes" ("Id") ON DELETE RESTRICT;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20181130220528_AddEmploymentType') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20181130220528_AddEmploymentType', '2.1.4-rtm-31024');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20181201163641_AddBackingFields') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20181201163641_AddBackingFields', '2.1.4-rtm-31024');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20181201174351_AddKeys') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20181201174351_AddKeys', '2.1.4-rtm-31024');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20181202153841_ChangeStringLengths') THEN
    ALTER TABLE "Vacancies" ALTER COLUMN "Description" TYPE character varying(5000);
    ALTER TABLE "Vacancies" ALTER COLUMN "Description" DROP NOT NULL;
    ALTER TABLE "Vacancies" ALTER COLUMN "Description" DROP DEFAULT;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20181202153841_ChangeStringLengths') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20181202153841_ChangeStringLengths', '2.1.4-rtm-31024');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20181202170616_RemoveDescriptionMaxLength') THEN
    ALTER TABLE "Vacancies" ALTER COLUMN "Description" TYPE text;
    ALTER TABLE "Vacancies" ALTER COLUMN "Description" DROP NOT NULL;
    ALTER TABLE "Vacancies" ALTER COLUMN "Description" DROP DEFAULT;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20181202170616_RemoveDescriptionMaxLength') THEN
    ALTER TABLE "Vacancies" ALTER COLUMN "ContactPerson" TYPE character varying(255);
    ALTER TABLE "Vacancies" ALTER COLUMN "ContactPerson" DROP NOT NULL;
    ALTER TABLE "Vacancies" ALTER COLUMN "ContactPerson" DROP DEFAULT;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20181202170616_RemoveDescriptionMaxLength') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20181202170616_RemoveDescriptionMaxLength', '2.1.4-rtm-31024');
    END IF;
END $$;
