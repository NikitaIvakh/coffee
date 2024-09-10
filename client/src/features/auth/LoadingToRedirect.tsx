import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'

const LoadingToRedirect = () => {
	const [count, setCount] = useState(5)
	const navigate = useNavigate()
	
	useEffect(() => {
		const interval = setInterval(() => {
			setCount((currentCount) => currentCount - 1)
		}, 1000)
		
		// eslint-disable-next-line @typescript-eslint/no-unused-expressions
		count === 0 && navigate('/Auth')
		return () => clearInterval(interval)
	}, [count, navigate])
	
	return (
		<div>
			<p>Redirecting your in {count} sec</p>
		</div>
	)
}

export default LoadingToRedirect