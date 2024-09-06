import { useCallback, useState } from 'react'

const useHttp = () => {
	const [process, setProcess] = useState('waiting')
	const request = async (url: string, method = 'GET', body = null, headers = { 'Content-type': 'application-json' }) => {
		try {
			setProcess('loading')
			const response = await fetch(url, { method, body, headers })
			
			if (!response.ok)
				throw Error(`Could not fetch ${url}, status: ${response.status}`)
			
			return await response.json()
			
		} catch (exception) {
			if (exception instanceof Error) {
				throw new Error(`${exception.name}: ${exception.message}`)
			}
			
			setProcess('error')
		}
	}
	
	const clearErrors = useCallback(() => {
		setProcess('loading')
	}, [])
	
	return { request, process, setProcess, clearErrors }
}

export default useHttp