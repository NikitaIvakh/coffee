import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import "./style/loadingRedirect.scss"

const LoadingToRedirect = () => {
	const [count, setCount] = useState(15)
	const navigate = useNavigate()
	
	useEffect(() => {
		const interval = setInterval(() => {
			setCount((currentCount) => currentCount - 1)
		}, 1000)
		
		if (count === 0) {
			navigate('/')
		}
		
		return () => clearInterval(interval)
	}, [count, navigate])
	
	return (
		<div className="loading-container">
			<div className="loading-content">
				<h1>Redirecting...</h1>
				<p>You will be redirected in <span>{count}</span> seconds.</p>
				<p className="info">If you are not redirected, <a href="/">click here</a> to go to the homepage.</p>
				<p className="warning">You are not authorized to view this page.</p>
			</div>
		</div>
	)
}

export default LoadingToRedirect
