// Save data to localStorage
function SaveData(key, value) {
    localStorage.setItem(key, value);
}

// Load data from localStorage
function LoadData(key) {
    return localStorage.getItem(key);
}
