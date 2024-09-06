import { CoffeeType } from './coffeeEnum'

export interface MyFormValues {
	name: string
	description: string
	price: number
	coffeeType: CoffeeType | string
	avatar: File | null
}

export interface MyTextInputProps {
	label: string
	id: string
	name: string
	type: string
}