import './header.scss'
import { NavLink } from 'react-router-dom'
import useLogout from '../../features/auth/use-logout.ts'
import ModalWindow from '../../features/modal/ModalWindow.tsx'
import useAuthModal from '../../features/modal/use-authModal.ts'

const Header = () => {
	const [user, handleLogout] = useLogout()
	const [authIsOpen, authOpenModalWindow, authCloseModalWindow] = useAuthModal()
	
	return (
		<header className='header'>
			<div className='header__wrapper'>
				<ul className='header__list'>
					<NavLink to='/' className='header__item'>Coffee house</NavLink>
					<NavLink to='/OurCoffee' className='header__item'>Our coffee</NavLink>
					<NavLink to='/Pleasure' className='header__item'>For your pleasure</NavLink>
				</ul>
				<ul className='header__admin'>
					{user?.userName && (
						<>
							<li className='header__userName'>Welcome, {user.userName}</li>
							<button type='button' onClick={handleLogout} className=' btn btn__filter header__button'>Logout</button>
						</>
					)}
					
					{!user?.userName && (
						<button onClick={authOpenModalWindow} className='btn btn__filter header__button'>Login</button>
					)}
					
					{authIsOpen && (
						<ModalWindow onClose={authCloseModalWindow} title='Login' isVisible={authIsOpen} />
					)}
					
					{(user?.role && user?.role === 'Administrator') && (
						<NavLink to='/AdminPanel' className='header__item'>Admin panel</NavLink>
					)}
				</ul>
			</div>
		</header>
	)
}

export default Header
