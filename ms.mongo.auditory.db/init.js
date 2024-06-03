db = db.getSiblingDB('auditory_db');
db.createUser(
    {
        user: "admin",
        pwd: "admin",
        roles: [
            {
                role: "readWrite",
                db: "auditory_db"
            }
        ]
    }
);
db.createCollection('users_registrations');
db.users_registrations.insert({ userName: 'rserrano', firstName: 'Ramón', lastName: 'Serrano Valero', lastRecord: '2024-05-15T08:00:00Z', mode: 'Entrada' });
