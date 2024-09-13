export type AuthRequestValues = {
	emailAddress: string,
	password: string,
}

export type AuthRegisterValues = {
	firstName: string,
	lastName: string,
	userName: string,
	emailAddress: string,
	password: string,
	passwordConform: string
}

export type AuthRefreshToken = {
	AccessToken: string,
	RefreshToken: string
}

export type AuthResponseValues = {
	id: string,
	firstName: string,
	lastName: string,
	userName: string,
	emailAddress: string,
	role: string
	jwtToken: string,
	refreshToken: string
}