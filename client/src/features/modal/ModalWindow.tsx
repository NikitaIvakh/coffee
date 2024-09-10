import React, { useEffect, useRef } from 'react'
import type { MouseEventHandler } from 'react'
import type { CoffeeItem } from '../../types'
import AdminForm from '../admin/AdminForm'
import Auth from '../auth/Auth.tsx'
import useAdminModal from './use-adminModal'
import useAuthModal from './use-authModal.ts'
import useCoffeesModal from './use-coffeesModal'
import './modalWindow.scss'

interface ModalWindowProps {
	onClose: MouseEventHandler<HTMLDivElement>;
	title: string;
	isVisible: boolean;
	coffee?: CoffeeItem | null;
}

const ModalWindow = ({ onClose, title, isVisible, coffee }: ModalWindowProps) => {
	const modalRef = useRef<HTMLDivElement | null>(null)
	const [adminIsOpen, , adminCloseModalWindow] = useAdminModal()
	const [coffeeIsOpen, , coffeeCloseModalWindow] = useCoffeesModal()
	const [authIsOpen, , authCloseModalWindow] = useAuthModal()
	
	const closeModal = () => {
		adminCloseModalWindow()
		coffeeCloseModalWindow()
		authCloseModalWindow()
	}
	
	const handleKeyDown = (event: React.KeyboardEvent<HTMLDivElement>) => {
		if (event.key === 'Escape') {
			closeModal()
		}
	}
	
	useEffect(() => {
		if (modalRef.current) {
			modalRef.current.focus()
		}
	}, [adminIsOpen, coffeeIsOpen, authIsOpen])
	
	return (
		<div
			className={`modal ${isVisible ? 'show' : 'hide'}`}
			tabIndex={-1}
			onKeyDown={handleKeyDown}
			ref={modalRef}
		>
			<div className='modal__dialog'>
				<div className='modal__content'>
					<div className='modal__close' onClick={onClose}>&times;</div>
					{coffeeIsOpen && (
						<AdminForm title={title} coffee={coffee} />
					)}
					
					{adminIsOpen && (
						<AdminForm title={title} coffee={coffee} />
					)}
					
					{authIsOpen && (
						<Auth />
					)}
				</div>
			</div>
		</div>
	)
}

export default ModalWindow
