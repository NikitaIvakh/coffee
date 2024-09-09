import './header.scss'
import { NavLink } from 'react-router-dom'
import ModalWindow from '../../features/modal/ModalWindow.tsx'
import useAuthModal from '../../features/modal/use-authModal.ts'

const Header = () => {
	const [authIsOpen, handleOpenAuthModal, handleCloseAuthModal] = useAuthModal()
	
	return (
		<header className='header'>
			<div className='header__wrapper'>
				<ul className='header__list'>
					<NavLink to='/' className='header__item'>Coffee house</NavLink>
					<NavLink to='/OurCoffee' className='header__item'>Our coffee</NavLink>
					<NavLink to='/Pleasure' className='header__item'>For your pleasure</NavLink>
				</ul>
				<ul className='header__admin'>
					<button onClick={handleOpenAuthModal}>Login</button>
					{authIsOpen && (
						<ModalWindow title="auth" isVisible={authIsOpen} onClose={handleCloseAuthModal}/>
					)}
					<NavLink to='/AdminPanel' className='header__item'>Admin panel</NavLink>
				</ul>
			</div>
		</header>
	)
}

export default Header
