import { useEffect, useState } from 'react'
import { NavLink, useNavigate } from 'react-router-dom'
import './style/loadingRedirect.scss'

type LoadingToRedirectProps = {
	message: string
}

const LoadingToRedirect = ({ message }: LoadingToRedirectProps) => {
	const [count, setCount] = useState(5)
	const navigate = useNavigate()
	
	useEffect(() => {
		const interval = setInterval(() => {
			setCount(currentCount => currentCount - 1)
		}, 1000)
		
		if (count === 0) {
			navigate('/')
		}
		
		return () => clearInterval(interval)
	}, [count, navigate])
	
	return (
		<div className='loading__container'>
			<div className='loading__content'>
				<h1 className='loading__header'>Redirecting...</h1>
				<p className='loading__message'>{message}</p>
				<p className='loading__redirectText'>You will be redirected in <span
					className='loading__counter'>{count}</span> seconds.</p>
				<p className='loading__info'>If you are not redirected, <NavLink to='/' className='loading__link'>click
					here</NavLink> to go to the homepage.</p>
			</div>
		</div>
	)
}

export default LoadingToRedirect
