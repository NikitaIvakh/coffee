export const BASE_URL = 'https://localhost:8081/api/coffee/GetCoffeeList'
const BASE_URL_ID = 'https://localhost:8081/api/coffee/GetCoffee'

export const ALL_COFFEES = (search: string, filter : string, page: number, pageSize: number) => `${BASE_URL}?search=${search}&filter=${filter}&page=${page}&pageSize=${pageSize}`
export const ALL_COFFEES_WITH_LIMIT = `${BASE_URL}?page=1&pageSize=3&limit=3`
export const ALL_COFFEES_WITH_SEARCH = (name: string) => `${BASE_URL}?search=${name}`
export const GET__COFFEE_BY_ID = (id: string) => BASE_URL_ID + `/${id}`

export const URL_CREATE_COFFEE = "https://localhost:8081/api/coffee/CreateCoffee"