import './header.scss'
import { useSelector } from 'react-redux'
import { NavLink, useNavigate } from 'react-router-dom'
import { selectAuthUser } from '../../features/auth/auth-Selectors.ts'
import { setLogout } from '../../features/auth/auth-slice.ts'
import { useLogoutMutation } from '../../service/authApi.ts'
import { useAppDispatch } from '../../store/store.ts'

const Header = () => {
	const { id, userName } = useSelector(selectAuthUser)
	const navigate = useNavigate()
	const dispatch = useAppDispatch()
	const [logout] = useLogoutMutation()
	
	const handleLogout = async () => {
		dispatch(setLogout())
		await logout(id!).unwrap()
		navigate('/Auth')
	}
	
	return (
		<header className='header'>
			<div className='header__wrapper'>
				<ul className='header__list'>
					<NavLink to='/' className='header__item'>Coffee house</NavLink>
					<NavLink to='/OurCoffee' className='header__item'>Our coffee</NavLink>
					<NavLink to='/Pleasure' className='header__item'>For your pleasure</NavLink>
				</ul>
				<ul className='header__admin'>
					<li>Welcome, {userName}</li>
					<button type='button' onClick={handleLogout}>Logout</button>
					<NavLink to='/AdminPanel' className='header__item'>Admin panel</NavLink>
				</ul>
			</div>
		</header>
	)
}

export default Header
