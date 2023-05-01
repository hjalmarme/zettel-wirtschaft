window.idbWrapper = (function () {
    // private
    const _dbName = 'ZettelWirtschaft';
    const _dbVersion = 1;
    let _db = null;

    // public
    return {
        open: function () {
            const openRequest = window.indexedDB.open(_dbName, _dbVersion);

            openRequest.onerror = function (event) {
                console.error('Database error: ' + event.target.errorCode);
            };

            openRequest.onsuccess = function (event) {
                _db = event.target.result;
                console.log('Database opened successfully');
            };

            openRequest.onupgradeneeded = function (event) {
                _db = event.target.result;

                _db.onerror = (event) => {
                    console.error('Database error: ' + event.target.errorCode);
                };

                _db.createObjectStore('menuItems', { keyPath: 'id' });

                //const objectStore = _db.createObjectStore('menuItems', { keyPath: 'id' });
                //objectStore.createIndex('type', 'type', { unique: false });
                //objectStore.createIndex('name', 'name', { unique: false });
                //objectStore.createIndex('description', 'description', { unique: false });
                //objectStore.createIndex('price', 'price', { unique: false });

                console.log('Object store created.');
            };
        },

        add: async function (objectStoreName, item) {
            console.log('Adding item to database...');

            const promiseAdd = new Promise((resolve, reject) => {
                const transaction = _db.transaction(objectStoreName, 'readwrite');

                transaction.oncomplete = () => {
                    console.log('Added item to database: ', item);
                    resolve(true);
                };

                transaction.onerror = () => {
                    console.log(`Transaction not opened due to error: ${transaction.error}`);
                    reject(false);
                };

                transaction.objectStore(objectStoreName).add(item);
            });

            return await promiseAdd;
        },

        get: async function (objectStoreName, id) {
            console.log('Retrieving item from database...');

            const promiseItem = new Promise((resolve, reject) => {
                let item = null;
                const transaction = _db.transaction(objectStoreName, 'readonly');

                transaction.oncomplete = (event) => {
                    console.log('Retrieved item from database:', item);
                    resolve(item);
                };

                transaction.onerror = () => {
                    console.log(`Transaction not opened due to error: ${transaction.error}`);
                    reject(item);
                };

                const getRequest = transaction.objectStore(objectStoreName).get(id);

                getRequest.onsuccess = () => item = getRequest.result;
            });

            return await promiseItem;
        },

        getAll: async function (objectStoreName) {
            console.log('Retrieving all items from database...');

            const promiseList = new Promise((resolve, reject) => {
                const items = [];
                const transaction = _db.transaction(objectStoreName, 'readonly');

                transaction.oncomplete = (event) => {
                    console.log('Retrieved all items from database: ', items);
                    resolve(items);
                };

                transaction.onerror = () => {
                    console.log(`Transaction not opened due to error: ${transaction.error}`);
                    reject(items);
                };

                const cursorRequest = transaction.objectStore(objectStoreName).openCursor();

                cursorRequest.onsuccess = (event) => {
                    const cursor = event.target.result;

                    if (!cursor) return;

                    items.push(cursor.value);
                    cursor.continue();
                };
            });

            return await promiseList;
        },

        put: async function (objectStoreName, item) {
            console.log('Updateing item in database...');

            const promisePut = new Promise((resolve, reject) => {
                const transaction = _db.transaction(objectStoreName, 'readwrite');

                transaction.oncomplete = () => {
                    console.log('Updated item in database: ', item);
                    resolve(true);
                };

                transaction.onerror = () => {
                    console.log(`Transaction not opened due to error: ${transaction.error}`);
                    reject(false);
                };

                const putRequest = transaction.objectStore(objectStoreName).put(item);

                putRequest.onsuccess = () => item = getRequest.result;
            });

            return await promisePut;
        },

        delete: async function (objectStoreName, id) {
            console.log('Deleting item from database...');

            const promiseDelete = new Promise((resolve, reject) => {
                const transaction = _db.transaction(objectStoreName, 'readwrite');

                transaction.oncomplete = () => {
                    console.log(`Item '${id}' deleted from database.`);
                    resolve(true);
                };

                transaction.onerror = () => {
                    console.log(`Transaction not opened due to error: ${transaction.error}`);
                    reject(false);
                };

                transaction.objectStore(objectStoreName).delete(id);
            });

            return await promiseDelete;
        },
    };
})();

window.onload = function () {
    window.idbWrapper.open();
};
