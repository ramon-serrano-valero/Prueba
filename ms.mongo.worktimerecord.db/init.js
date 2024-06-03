db = db.getSiblingDB('work_time_db');
db.createUser(
    {
        user: "admin",
        pwd: "admin",
        roles: [
            {
                role: "readWrite",
                db: "work_time_db"
            }
        ]
    }
);
db.createCollection('users_last_registry');
db.users_last_registry.insert({ userName: 'rserrano', firstName: 'Ramón', lastName: 'Serrano Valero', lastRecord: '2024-05-15T08:00:00Z', mode: 'Entrada' });
