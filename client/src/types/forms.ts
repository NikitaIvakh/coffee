import { CoffeeType } from './coffeeEnum'

export interface MyFormValues {
	id?: string | undefined
	name: string
	description: string
	price: number
	coffeeType: CoffeeType | string
	avatar: File | null
}

export interface MyTextInputProps {
	label: string,
	id: string,
	name: string,
	type: string,
	value?: string
}

export interface MtAuthFormValues {
	emailAddress: string
	password: string
}

export interface MyRegisterValues {
	firstName: string,
	lastName: string,
	userName: string,
	emailAddress: string,
	password: string,
	passwordConform: string
}

export type LoginOutputValue = {
	firstName: string,
	lastName: string,
	userName: string,
	emailAddress: string,
	jwtToken: string,
	refreshToken: string
}

export type LoginOutputErrors = {
	code: string,
	message: string,
}

export type LoginOutput = {
	value: LoginOutputValue,
	isSuccess: boolean,
	isFailure: boolean,
	error: LoginOutputErrors
}

export interface AuthSliceType {
	userName: string | null;
	jwtToken: string | null;
	refreshToken: string | null;
}