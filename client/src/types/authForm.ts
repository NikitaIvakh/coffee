export type AuthResponse = {
	value: AuthResponseValue,
	isSuccess: boolean,
	isFailure: boolean,
	error: AuthResponseError
}

export type AuthResponseValue = {
	accessToken: string
	refreshToken: string
	user: User
}

export type AuthResponseError = {
	code: string,
	message: string
}

export type AuthRequestValues = {
	emailAddress: string,
	password: string,
}

export type User = {
	userId: string,
	userName: string,
	emailAddress: string
}