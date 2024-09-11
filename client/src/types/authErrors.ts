export type AuthErrors = {
	data: {
		type: string,
		title: string,
		status: number,
		detail: string,
		errors: [] | null
	}
}

export type RegisterErrors = {
	data: {
		type: string,
		title: string,
		status: number,
		detail: string,
		errors: [] | null
	}
}