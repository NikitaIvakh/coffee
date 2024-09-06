export type CoffeeItem = {
	id: string,
	name: string,
	coffeeType: string,
	price: number,
	createdAt: string,
	imageUrl: string,
	imageLocalPath: string
};

export type Coffee = {
	value: {
		items: CoffeeItem[],
		page: number,
		pageSize: number,
		totalCount: number,
		hasNextPage: boolean,
		hasPreviousPage: boolean
	},
}

export type CoffeeById = {
	"id": string,
	"name": string,
	"coffeeType": string,
	"description": string,
	"price": number,
	"imageUrl": string,
	"imageLocalPath": string
}