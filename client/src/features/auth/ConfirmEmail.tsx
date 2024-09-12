import { useEffect, useState } from 'react'
import { NavLink, useNavigate } from 'react-router-dom'
import useAuth from './use-auth'
import './style/confirmEmail.scss'

const ConfirmEmail = () => {
	const [counter, setCounter] = useState<number | null>(null) // Counter starts as null
	const navigate = useNavigate()
	const [, , , , , handleConfirmEmail, user, isEmailConfirmed] = useAuth()
	
	useEffect(() => {
		let interval: NodeJS.Timeout | undefined
		
		if (counter !== null && counter > 0) {
			interval = setInterval(() => {
				setCounter(prevCounter => (prevCounter !== null ? prevCounter - 1 : 0))
			}, 1000)
		} else if (counter === 0) {
			navigate('/')
		}
		
		return () => clearInterval(interval)
	}, [counter, navigate])
	
	const handleClick = async () => {
		try {
			await handleConfirmEmail(user.id)
			setCounter(10)
		} catch (error) {
			console.error('Error confirming email:', error)
		}
	}
	
	return (
		<div className='confirm-email__container'>
			<div className='confirm-email__content'>
				{!isEmailConfirmed ? (
					<button className='confirm-email__btn' onClick={handleClick}>Confirm Email</button>
				) : (
					<>
						<h1 className='confirm-email__header'>Email Confirmed</h1>
						{counter !== null && counter > 0 && (
							<p className='confirm-email__redirectText'>You will be redirected in
								<span className='confirm-email__counter'> {counter} </span>
								seconds.
							</p>
						)}
						<p className='confirm-email__info'>
							If you are not redirected, <NavLink to='/' className='confirm-email__link'>click here</NavLink> to go to
							the homepage.
						</p>
					</>
				)}
			</div>
		</div>
	)
}

export default ConfirmEmail
