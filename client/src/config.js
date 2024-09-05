const BASE_URL = 'https://localhost:8081/api/coffee/GetCoffeeList'
const BASE_URL_ID = 'https://localhost:8081/api/coffee/GetCoffee'

export const ALL_COFFEES = BASE_URL
export const ALL_COFFEES_WITH_LIMIT = BASE_URL + '?Limit=3'
export const ALL_COFFEES_WITH_SEARCH = (name) => BASE_URL + `?search=${name}`
export const GET__COFFEE_BY_ID = (id) => BASE_URL_ID + `/${id}`